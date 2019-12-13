using Microsoft.AspNetCore.Mvc;
using Portfolio_Website_Core.Models;
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
        public string Index() // This is an Action method. and it handles what happens with the incoming https request
        {
            return _employeeRepository.GetEmployee(1).Name;
        }

        public ViewResult Details() // This is an Action method. and it handles what happens with the incoming https request
        {
            Employee model = _employeeRepository.GetEmployee(1);

            return View(model);
        }
    }
}
