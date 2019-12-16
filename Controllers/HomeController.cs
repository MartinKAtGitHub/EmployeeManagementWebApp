using Microsoft.AspNetCore.Mvc;
using Portfolio_Website_Core.Models;
using Portfolio_Website_Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio_Website_Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository; // Read only. because we don't want to change the data in here

        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // Action methods need to have view(razor page) with a similar page. 

        public ViewResult Index() // This is an Action method. and it handles what happens with the incoming https request
        {
            var model = _employeeRepository.GetAllEmployeesList();
            return View(model);
        }


        public ViewResult Details(int? id) // This is an Action method. and it handles what happens with the incoming https request
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = _employeeRepository.GetEmployee(id??1),
                PageTitle = "Employee Details"
            };

            return View(homeDetailsViewModel);

            //Employee LOL = _employeeRepository.GetEmployee(1);
            //ViewBag.PageTitle = "Employee Details";
            //return View(LOL);
        }
    }
}
