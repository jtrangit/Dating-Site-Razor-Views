using Dating_Site_Razor_Views.Models;
using DatingSiteLibrary;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Utilities;
using System;

namespace Dating_Site_Razor_Views.Controllers
{
    public class RegisterController : Controller
    {
        private readonly Dating _dating;

        public RegisterController()
        {
            _dating = new Dating();
        }

        public IActionResult Register()
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
                return RedirectToAction("Login", "Home"); 
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Registration failed. Please try again later.");
                return View(model);
            }
        }
    }
}
