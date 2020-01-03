using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio_Website_Core.Models
{
    // 77 Custom / Extended Identity user to add extra property that doesn't come with the baseline Identity user class
    public class ApplicationUser : IdentityUser
    {       
        public string City { get; set; } // Remember to add-migrate adn update-db to create a columns for the new prop
    }
}
