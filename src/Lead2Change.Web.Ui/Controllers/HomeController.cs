﻿using Lead2Change.Domain.Models;
using Lead2Change.Services.Identity;
using Lead2Change.Web.Ui.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Lead2Change.Web.Ui.Controllers
{
    public class HomeController : _BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUserService identityService, RoleManager<AspNetRoles> roleManager, UserManager<AspNetUsers> userManager, SignInManager<AspNetUsers> signInManager) : base(identityService, roleManager, userManager, signInManager)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
