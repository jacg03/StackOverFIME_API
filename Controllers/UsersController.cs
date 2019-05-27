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
    public class UsersController : ApiController
    {
        private StackOverFIMEEntities db = new StackOverFIMEEntities();

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(Guid id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST; api/Users/Login
        [HttpPost]
        [ResponseType(typeof(ResponseLogin))]
        [Route("api/Users/Login")]
        public async Task<IHttpActionResult> Login(Credentials credentials)
        {
            User result = await db.Users.Where(x => x.UserName == credentials.UserName && x.Password == credentials.Password).FirstAsync();
            if (result != null)
            {
                ResponseLogin data = new ResponseLogin();
                data.Name = result.Name;
                data.Id = result.Id;

                return Ok(data);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(Guid id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [HttpPost]
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            user.Id = System.Guid.NewGuid();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (db.Users.Where(x => x.UserName == user.UserName).ToList().Count == 0)
            {
                db.Users.Add(user);
                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (db.Users.Where(x => x.UserName == user.UserName).ToList().Count > 0)
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                return Conflict();
            }

            User result = db.Users.Where(x => x.UserName == user.UserName).FirstOrDefault();

            return Ok(result);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(Guid id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(Guid id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}

public class Credentials
{
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class ResponseLogin
{
    public string Name { get; set; }
    public Guid Id { get; set; }
}