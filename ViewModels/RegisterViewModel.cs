using Microsoft.AspNetCore.Mvc;
using Portfolio_Website_Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio_Website_Core.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Account")] // 75 Remote validation used to check in Email is in use
        public string Email{ get; set; }

        [CustomValidEmail(allowedDomain: "gmail.com",
            ErrorMessage = "The Email domain must be gmail.com")]
        public string CustomEmail { get; set; } // Special Email that only takes a certain domaine name


        public string City { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Required] don't need this because we are comparing PW to this
        [DataType(DataType.Password)]
        [Display(Name ="Confirm password")]
        [Compare("Password", ErrorMessage ="Doesn't Match")]
        public string ConfirmPassword { get; set; }


    }
}
