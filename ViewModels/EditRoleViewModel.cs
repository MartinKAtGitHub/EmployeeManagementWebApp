﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio_Website_Core.ViewModels
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users = new List<string>();
        }
        public string Id { get; set; }
        
        [Required(ErrorMessage ="Role name is required")]
        [Display(Name ="Role name")]
        public string RoleName { get; set; }

        public List<String> Users { get; set; }
    }
}
