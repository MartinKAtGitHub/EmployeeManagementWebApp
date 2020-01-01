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
        public string Email{ get; set; }

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
