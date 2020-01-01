using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio_Website_Core.ViewModels
{
    public class LoginViewModel
    {
        /// <summary>
        /// Email Is the user name in this
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe{ get; set; } // Do want to use persistent Cookie or session cookie

    }
}
