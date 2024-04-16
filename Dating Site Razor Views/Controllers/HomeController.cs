using Dating_Site_Razor_Views.Models;
using Dating_Site_Razor_Views.Pages;
using DatingSiteLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using Utilities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dating_Site_Razor_Views.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBConnect _dbConnect;
        private readonly Dating _dating;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _dbConnect = new DBConnect();
            _dating = new Dating();

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult ViewProfile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegistrationValidation model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                _dating.createAccount(model.Username, model.Password, model.FirstName, model.LastName, model.Email);
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Registration failed. Please try again later.");
                return View(model);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
