using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio_Website_Core.Models
{
    public class Comment
    {
        public string Id { get; set; } // Might not need this since a comment needs a user
        public string UserId { get; set; } // The user who posted. -> FK This is also what we will use to filter the result on a page
        public string ViewId { get; set; } // This would be the ID to get to the Employee ID details view. 
       
        [MaxLength(300), Required]
        public string Text { get; set; }
    }
}
