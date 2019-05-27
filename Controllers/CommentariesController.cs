using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using StackOverFIME_API.Models;

namespace StackOverFIME_API.Controllers
{
    public class CommentariesController : ApiController
    {
        private StackOverFIMEEntities db = new StackOverFIMEEntities();

        // GET: api/Commentaries
        [HttpGet]
        [ResponseType(typeof(List<SP_GetInitialCommentariesWithDetails_Result>))]
        public List<SP_GetInitialCommentariesWithDetails_Result> GetCommentaries()
        {
            return db.SP_GetInitialCommentariesWithDetails().ToList();
        }

        // GET: api/Commentaries/5
        [HttpGet]
        [ResponseType(typeof(SP_GetInitialCommentary_Result))]
        public async Task<IHttpActionResult> GetInitialCommentary(Guid id)
        {
            SP_GetInitialCommentary_Result commentary = db.SP_GetInitialCommentary(id).FirstOrDefault();

            if (commentary == null)
            {
                return NotFound();
            }

            return Ok(commentary);
        }

        // GET: api/Commentaries/GetInitialsByUserId/{id}
        [HttpGet]
        [ResponseType(typeof(List<SP_GetInitialCommentariesWithDetails_Result>))]
        [Route("api/Commentaries/GetInitialsByUserId/{id}")]
        public async Task<IHttpActionResult> GetInitialsByUserId(Guid id)
        {
            var commentaries = db.SP_GetInitialCommentariesWithDetails().AsQueryable();

            var result = commentaries.Where(x => x.UserId == id).ToList();

            return Ok(result);
        }

        // GET: api/Commentaries/GetByInitialId/{id}
        [HttpGet]
        [ResponseType(typeof(List<SP_GetAnswersOfInitialCommentary_Result>))]
        [Route("api/Commentaries/GetByInitialId/{id}")]
        public async Task<IHttpActionResult> GetByInitialId(Guid id)
        {
            List<SP_GetAnswersOfInitialCommentary_Result> commentariesWithUserName = db.SP_GetAnswersOfInitialCommentary(id).ToList();
            if (commentariesWithUserName == null)
            {
                return NotFound();
            }

            return Ok(commentariesWithUserName);
        }

        //// GET: api/Commentaries/GetByUserId/{id}
        //[ResponseType(typeof(List<Commentary>))]
        //[HttpGet]
        //[Route("api/Commentaries/GetByUserId/{id}")]
        //public async Task<IHttpActionResult> GetByUserId(Guid id)
        //{
        //    List<Commentary> commentaries = await db.Commentaries.Where(x => x.UserId == id).ToListAsync();
        //    if (commentaries == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(commentaries);
        //}


        // GET: api/Commentaries/Search/{text}
        [HttpGet]
        [ResponseType(typeof(List<SP_SearchInitialCommentaries_Result>))]
        [Route("api/Commentaries/Search/{text}")]
        public async Task<IHttpActionResult> Search(string text)
        {
            var result = db.SP_SearchInitialCommentaries(text).ToList();

            return Ok(result);
        }

        // POST: api/Commentaries
        [HttpPost]
        public async Task<IHttpActionResult> PostCommentary(Commentary InitialCommentary)
        {
            InitialCommentary.Id = System.Guid.NewGuid();
            InitialCommentary.CreatedAt = DateTime.Now;
            InitialCommentary.UpdatedAt = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Commentaries.Add(InitialCommentary);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CommentaryExists(InitialCommentary.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            
            return Ok();
        }

        // DELETE: api/Commentaries/5
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCommentary(Guid id)
        {
            Commentary commentary = await db.Commentaries.FindAsync(id);
            if (commentary == null)
            {
                return NotFound();
            }

            db.Commentaries.Remove(commentary);
            await db.SaveChangesAsync();

            return Ok(commentary);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommentaryExists(Guid id)
        {
            return db.Commentaries.Count(e => e.Id == id) > 0;
        }

        //public class InitialCommentary
        //{
        //    public Commentary commentary { get; set; }
        //    public List<Tag> tags { get; set; }
        //}
    }
}