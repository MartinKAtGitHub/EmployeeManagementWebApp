using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio_Website_Core.Models
{
    public class SQLCommentRepository : ICommentRepository
    {
        private readonly AppDdContext appDdContext;

        public SQLCommentRepository(AppDdContext appDdContext)
        {
            this.appDdContext = appDdContext;
        }

        public Comment CreateComment(Comment comment)
        {
            appDdContext.Comments.Add(comment);
            appDdContext.SaveChanges();
            return comment;
        }

        public Comment DeleteComment(string Id)
        {
            throw new NotImplementedException();
        }

        public Comment ReadComment(string Id)
        {
            throw new NotImplementedException();
        }

        public Comment UpdateComment(Comment UpdatedComment)
        {
            throw new NotImplementedException();
        }
    }
}
