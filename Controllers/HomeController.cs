using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio_Website_Core.Models;
using Portfolio_Website_Core.Security;
using Portfolio_Website_Core.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio_Website_Core.Controllers
{
    //[Authorize] Can be used on the whole controller
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository; // Read only. because we don't want to change the data in here
        private readonly ICommentRepository commentRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly ILogger logger;

        private readonly IDataProtector protector;

        public HomeController(
            IEmployeeRepository employeeRepository,
            ICommentRepository commentRepository,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IWebHostEnvironment hostingEnvironment,
            ILogger<HomeController> logger,
            IDataProtectionProvider dataProtectionProvider,
            DataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            _employeeRepository = employeeRepository;
            this.commentRepository = commentRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.hostingEnvironment = hostingEnvironment;
            this.logger = logger;

            //120
            protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.EmployeeIdRouteValue);
        }


        [HttpGet]
        public IActionResult PostComment(string id)
        {
            return View(/*id*/);
        }

        [HttpPost]
        public async Task<IActionResult> PostComment(Comment comment, string id)
        {
            var currentUser =  await userManager.GetUserAsync(User);

            var newComment = new Comment
            {
                Id = Guid.NewGuid().ToString(),
                Text = comment.Text,
                UserId = currentUser.Id,
                ViewId = id
            };

            commentRepository.CreateComment(newComment);

            return RedirectToAction("details", new { id = protector.Protect(newComment.ViewId) });
        }



        // Action methods need to have view(razor page) with a similar page. 
        [AllowAnonymous]
        public ViewResult Index() // This is an Action method. and it handles what happens with the incoming https request
        {
            var model = _employeeRepository.GetAllEmployees()

                 .Select(e =>
                 {
                     // Encrypt the ID value and store in EncryptedId property
                     e.EncryptedId = protector.Protect(e.Id.ToString());
                     return e;
                 });
            return View(model);
        }

        //public ViewResult Details(int? id) // This is an Action method. and it handles what happens with the incoming https request
        [AllowAnonymous]
        public ViewResult Details(string id) // String ID   // 120 encryption and decryption
        {
            //  throw new Exception("Creating an Exception");
           
            // If ouer ID = NULL we just throw a 404 and as the user to go back or somthing

            // Decrypt the employee id using Unprotected method
            string decryptedId = protector.Unprotect(id);
            int decryptedIntId = Convert.ToInt32(decryptedId);


            var emp = _employeeRepository.GetEmployee(decryptedIntId);
            if (emp == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", decryptedIntId);
            }

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = emp,
                PageTitle = "Employee Details",
                comments = commentRepository.GetAllCommentsOnEmployeeId(emp.Id.ToString())
            };

            return View(homeDetailsViewModel);

            //Employee LOL = _employeeRepository.GetEmployee(1);
            //ViewBag.PageTitle = "Employee Details";
            //return View(LOL);
        }

        [HttpGet]
        [Authorize]
        public ViewResult Create()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ViewResult Edit(int Id)
        {
            var emp = _employeeRepository.GetEmployee(Id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = emp.Id,
                Name = emp.Name,
                Email = emp.Email,
                Department = emp.Department,
                ExistingPhotoPath = emp.PhotoPath
            };

            return View(employeeEditViewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid) // Checks if all the required fields are valid
            {

                Employee employee = _employeeRepository.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;

                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    employee.PhotoPath = ProccessUploadedFile(model);
                }

                _employeeRepository.Update(employee);

                return RedirectToAction("index");
            }

            return View();
        }

        private string ProccessUploadedFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images"); // This will find the wwwroot/images
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;

                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create)) // this line makes sure the Filestream is done doing what it needs to do before copying
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        //[HttpPost]
        //public IActionResult Create(Employee employee)
        //{
        //    if(ModelState.IsValid) // Checks if all the required fields are valid
        //    {
        //        var newEmp = _employeeRepository.AddEmployee(employee);
        //        var parameterData = new { 
        //            id = newEmp.Id,
        //            CheckURL = "KEKW"
        //        };

        //        return RedirectToAction("details", parameterData);
        //    }

        //    return View();
        //}

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid) // Checks if all the required fields are valid
            {
                string uniqueFileName = ProccessUploadedFile(model);
                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };

                _employeeRepository.AddEmployee(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _employeeRepository.Delete(id);
            return RedirectToAction("index");
        }
    }
}
