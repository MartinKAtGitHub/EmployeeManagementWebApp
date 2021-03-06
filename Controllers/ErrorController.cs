﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Portfolio_Website_Core.Controllers
{
    // 58
    public class ErrorController : Controller
    {
        private readonly IConfiguration config;
        private readonly ILogger<ErrorController> logger;

        // 62
        public ErrorController(IConfiguration config, ILogger<ErrorController> logger) // Generics are used to group the log messages into this class type
        {
            this.config = config;
            this.logger = logger;
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
           
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Cant find your page bruh";
                    ViewBag.Enviroment =  config.GetSection("ASPNETCORE_ENVIRONMENT").Value;
                    ViewBag.Path =  statusCodeResult.OriginalPath;
                    ViewBag.QS = statusCodeResult.OriginalQueryString;

                    logger.LogWarning($"404 Error occurred. Path = {statusCodeResult.OriginalPath}" +
                        $" QS = { statusCodeResult.OriginalQueryString} ");
                    break;
            }

            return View("NotFound");
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exeptionDetailes = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            logger.LogError($"The path {exeptionDetailes.Path} threw an exception {exeptionDetailes.Error} ");

            ViewBag.ExceptionPath = "Path = " + exeptionDetailes.Path;
            ViewBag.ExceptionMessage =  "Error Message = " + exeptionDetailes.Error.Message;
            //ViewBag.Stacktrace = exeptionDetailes.Error.StackTrace;
            return View("Error");
        }
    }
}
