using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio_Website_Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio_Website_Core.Controllers
{
    // 78
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<AdministrationController> logger;

        public AdministrationController(RoleManager<IdentityRole> roleManager, ILogger<AdministrationController> logger)
        {
            this.roleManager = roleManager;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if(ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                
                if(result.Succeeded)
                {
                    return RedirectToAction("ListRoles","Administration");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description); // This will send errors to view validation(red text)
                }
            }

           
            return View();
        }
        [HttpGet]
        public IActionResult listRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }
    }
}
