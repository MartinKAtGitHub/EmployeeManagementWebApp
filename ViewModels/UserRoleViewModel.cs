using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio_Website_Core.ViewModels
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        //public string RoleId { get; set; } // we use viewbag to avoid duplicating this for every user
        public string UserName { get; set; }
        public bool IsSelected { get; set; }

    }
}
