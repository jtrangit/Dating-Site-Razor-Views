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
            if (ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                string username = model.Username;
                string password = model.Password;
                string firstname = model.FirstName;
                string lastname = model.LastName;
                string email = model.Email;
                string sq1 = model.SecurityAnswer1;
                string sq2 = model.SecurityAnswer2;
                string sq3 = model.SecurityAnswer3;

                Dating checkUniqueUsername = new Dating();

                if (checkUniqueUsername.validateUniqueUsername(username) == 1)
                {
                    ModelState.AddModelError(nameof(RegistrationValidation.Username), "Username is not unique");
                    return View(model);
                    
                }
                else
                {
                    
                    _dating.createAccount(username, password, firstname, lastname, email);

                    Dating newProfile = new Dating();
                    DataSet newProfileDs = new DataSet();
                    int accountID;


                    //creates account record for login
                    //This is also where we would encrypt the password
                    //code would go here
                    newProfileDs = newProfile.getUserInfo(username, password);
                    accountID = Convert.ToInt32(newProfileDs.Tables[0].Rows[0]["Id"].ToString());

                    //creates empty profile record for the new account
                    string fullname = firstname + " " + lastname;

                    Dating theProfile = new Dating();
                    theProfile.createProfile(accountID, email, fullname);

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
                    securityQuestions.createSecurityQuestions(accountID, sq1, sq2, sq3);

                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Registration failed. Please try again later.");
                return View(model);
            }
        }
    }
}
