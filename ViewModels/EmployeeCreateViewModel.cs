using Microsoft.AspNetCore.Http;
using Portfolio_Website_Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio_Website_Core.ViewModels
{
    public class EmployeeCreateViewModel
    {
        [Required]
        [MaxLength(20, ErrorMessage = "Name needs to be less then 50")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Office Email")]
        public string Email { get; set; }
        public Dept Department { get; set; }
        public IFormFile Photo { get; set; } // This receives the file
    }
}
