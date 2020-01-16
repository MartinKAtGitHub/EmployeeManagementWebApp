using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio_Website_Core.Models
{
    interface ICommentRepository
    {
        Comment ReadComment(string Id); // user ID insted
        Comment CreateComment(Comment comment);
        Comment UpdateComment(Comment UpdatedComment);
        Comment DeleteComment(string Id);
    }
}
