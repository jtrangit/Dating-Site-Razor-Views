using Dating_Site_Razor_Views.Models;
using DatingSiteLibrary;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Utilities;
using System;
using System.Diagnostics;

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
                /*
                _dating.createAccount(model.Username, model.Password, model.FirstName, model.LastName, model.Email);

                Dating newProfile = new Dating();
                DataSet newProfileDs = new DataSet();
                int accountID;

                //creates account record for login
                //This is also where we would encrypt the password
                //code would go here
                newProfileDs = newProfile.getUserInfo(model.Username, model.Password);
                accountID = Convert.ToInt32(newProfileDs.Tables[0].Rows[0]["Id"].ToString());

                //creates empty profile record for the new account
                Dating theProfile = new Dating();
                theProfile.createProfile(accountID, model.Email);

                //creates empty interests record for the new account
                Dating theInterests = new Dating();
                theInterests.createInterests(accountID);

                //creates empty dislikes record for the new account
                Dating theDislikes = new Dating();
                theDislikes.createDatingDislikes(accountID);

                //creates empty profile questions record for the new account
                Dating profileQuestions = new Dating();
                profileQuestions.createProfileQuestions(accountID);

                //stores the security questions to table
                Dating securityQuestions = new Dating();
                securityQuestions.createSecurityQuestions(accountID, model.SecurityAnswer1, model.SecurityAnswer2, model.SecurityAnswer3);
                
                */
                Debug.WriteLine("answer to security questions " + model.SecurityAnswer1 + ", " +  model.SecurityAnswer2 + ", " + model.SecurityAnswer3);

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
