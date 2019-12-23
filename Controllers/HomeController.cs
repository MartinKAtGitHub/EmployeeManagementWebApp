using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Portfolio_Website_Core.Models;
using Portfolio_Website_Core.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio_Website_Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository; // Read only. because we don't want to change the data in here
        private readonly IWebHostEnvironment hostingEnvironment;

        public HomeController(IEmployeeRepository employeeRepository, IWebHostEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        // Action methods need to have view(razor page) with a similar page. 

        public ViewResult Index() // This is an Action method. and it handles what happens with the incoming https request
        {
            var model = _employeeRepository.GetAllEmployeesList();
            return View(model);
        }


        public ViewResult Details(int? id) // This is an Action method. and it handles what happens with the incoming https request
        {

            var emp = _employeeRepository.GetEmployee(id.Value);
            if(emp == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = emp,
                PageTitle = "Employee Details"
            };

            return View(homeDetailsViewModel);

            //Employee LOL = _employeeRepository.GetEmployee(1);
            //ViewBag.PageTitle = "Employee Details";
            //return View(LOL);
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpGet]
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
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid) // Checks if all the required fields are valid
            {

                Employee employee = _employeeRepository.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;

                if(model.Photo != null)
                {
                    if(model.ExistingPhotoPath != null)
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
                return RedirectToAction("details", new { id = newEmployee.Id});
            }

            return View();
        }

    }
}
