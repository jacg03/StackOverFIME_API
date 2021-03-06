﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StackOverFIME_API.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class StackOverFIMEEntities : DbContext
    {
        public StackOverFIMEEntities()
            : base("name=StackOverFIMEEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Commentary> Commentaries { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }
    
        public virtual ObjectResult<SP_CommentaryWithUserName_Result> SP_CommentaryWithUserName()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_CommentaryWithUserName_Result>("SP_CommentaryWithUserName");
        }
    
        public virtual ObjectResult<SP_GetCommentariesOfInitialCommentary_Result> SP_GetCommentariesOfInitialCommentary(Nullable<System.Guid> initialId)
        {
            var initialIdParameter = initialId.HasValue ?
                new ObjectParameter("InitialId", initialId) :
                new ObjectParameter("InitialId", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GetCommentariesOfInitialCommentary_Result>("SP_GetCommentariesOfInitialCommentary", initialIdParameter);
        }
    
        public virtual ObjectResult<SP_GetInitialCommentaryDetails_Result> SP_GetInitialCommentaryDetails(Nullable<System.Guid> commentaryId)
        {
            var commentaryIdParameter = commentaryId.HasValue ?
                new ObjectParameter("CommentaryId", commentaryId) :
                new ObjectParameter("CommentaryId", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GetInitialCommentaryDetails_Result>("SP_GetInitialCommentaryDetails", commentaryIdParameter);
        }
    
        public virtual ObjectResult<SP_GetCommentariesWithDetails_Result> SP_GetCommentariesWithDetails()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GetCommentariesWithDetails_Result>("SP_GetCommentariesWithDetails");
        }
    
        public virtual ObjectResult<SP_SearchCommentaries_Result> SP_SearchCommentaries(string t)
        {
            var tParameter = t != null ?
                new ObjectParameter("T", t) :
                new ObjectParameter("T", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_SearchCommentaries_Result>("SP_SearchCommentaries", tParameter);
        }
    
        public virtual ObjectResult<SP_GetAnswersOfInitialCommentary_Result> SP_GetAnswersOfInitialCommentary(Nullable<System.Guid> initialId)
        {
            var initialIdParameter = initialId.HasValue ?
                new ObjectParameter("InitialId", initialId) :
                new ObjectParameter("InitialId", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GetAnswersOfInitialCommentary_Result>("SP_GetAnswersOfInitialCommentary", initialIdParameter);
        }
    
        public virtual ObjectResult<SP_GetInitialCommentariesWithDetails_Result> SP_GetInitialCommentariesWithDetails()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GetInitialCommentariesWithDetails_Result>("SP_GetInitialCommentariesWithDetails");
        }
    
        public virtual ObjectResult<SP_GetInitialCommentary_Result> SP_GetInitialCommentary(Nullable<System.Guid> commentaryId)
        {
            var commentaryIdParameter = commentaryId.HasValue ?
                new ObjectParameter("CommentaryId", commentaryId) :
                new ObjectParameter("CommentaryId", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GetInitialCommentary_Result>("SP_GetInitialCommentary", commentaryIdParameter);
        }
    
        public virtual ObjectResult<SP_SearchInitialCommentaries_Result> SP_SearchInitialCommentaries(string t)
        {
            var tParameter = t != null ?
                new ObjectParameter("T", t) :
                new ObjectParameter("T", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_SearchInitialCommentaries_Result>("SP_SearchInitialCommentaries", tParameter);
        }
    }
}
