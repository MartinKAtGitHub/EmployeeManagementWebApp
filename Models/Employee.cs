using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio_Website_Core.Models
{
    public class Employee
    {
        public int Id { get; set; }
        //120
        [NotMapped] // only used to encrypt the ID so we don't want to map it for model binding
        public string EncryptedId { get; set; } 

        [Required]
        [MaxLength(20, ErrorMessage ="Name needs to be less then 50")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name ="Office Email")]
        public string Email { get; set; }
        public Dept Department { get; set; }
        /// <summary>
        /// Functions more like PHOTONAME
        /// </summary>
        public string PhotoPath { get; set; } 
    }
}
