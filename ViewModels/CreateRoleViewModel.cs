﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio_Website_Core.ViewModels
{
    //78
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
