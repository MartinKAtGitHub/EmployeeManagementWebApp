using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Portfolio_Website_Core.Controllers
{
    public class ErrorController : Controller
    {
        private readonly IConfiguration config;

        public ErrorController(IConfiguration config)
        {
            this.config = config;
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
           
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Cant find your page bruh";
                    ViewBag.Enviroment =  config.GetSection("ASPNETCORE_ENVIRONMENT").Value;
                    break;
            }

            return View("NotFound");
        }
    }
}
