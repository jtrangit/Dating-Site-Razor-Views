using Dating_Site_Razor_Views.Models;
using DatingSiteLibrary;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;

namespace Dating_Site_Razor_Views.Controllers
{
    public class ForgotPasswordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View("~/Views/ForgotPassword/ForgotPassword.cshtml");
        }

        public IActionResult SecurityQuestion(string email)
        {
            Dating checkEmail = new Dating();

            List<string> securityQuestions = new List<string>();
            securityQuestions.Add("What was your first pet's name?");
            securityQuestions.Add("What was your mother's maiden name?");
            securityQuestions.Add("What is your favorite food?");

            string invalidEmail = string.Empty;

            if (checkEmail.getValidEmail(email).Equals(1)) //valid email
            {
                Random randomQuestion = new Random();
                string securityQuestion = securityQuestions[randomQuestion.Next(0, 3)];
                TempData["SecurityQuestion"] = securityQuestion;

                TempData["TheEmailToReset"] = email;

                return View("~/Views/ForgotPassword/SecurityQuestion.cshtml");
            }
            else //email not found
            {
                invalidEmail = "Email does not belong to an account";
                TempData["InvalidEmail"] = invalidEmail;
                return View("~/Views/ForgotPassword/ForgotPassword.cshtml");
            }
        }

        [HttpPost]
        public IActionResult SendResetEmail()
        {
            string email = Request.Form["theEmail"];
            string secQuestion = Request.Form["txtSecurityQuestion"];
            string secAnswer = Request.Form["securityAnswer"];

            //get the account ID of the account with the Email
            Dating getAccID = new Dating();
            DataSet ds = new DataSet();
            ds = getAccID.getLoginFromEmail(email);

            int accID = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"]);

            if (secQuestion == "What was your first pet's name?")
            {
                Dating checkAnswer = new Dating();
                
                if (checkAnswer.getSecurityAnswer1(secAnswer, accID).Equals(1)) //answer is valid
                {
                    //send email
                    return View("~/Views/Home/login.cshtml");
                }
                else //answer is not valid 
                {  
                    return View("~/Views/ForgotPassword/ForgotPassword.cshtml", ViewData["InvalidEmail"]);
                }
            }
            else if (secQuestion == "What was your mother's maiden name?")
            {
                Dating checkAnswer = new Dating();

                if (checkAnswer.getSecurityAnswer2(secAnswer, accID).Equals(1)) //answer is valid
                {
                    //send email
                    return View("~/Views/Home/login.cshtml");
                }
                else //answer is not valid 
                {
                    return View("~/Views/ForgotPassword/ForgotPassword.cshtml", ViewData["InvalidEmail"]);
                }
            }
            else
            {
                Dating checkAnswer = new Dating();

                if (checkAnswer.getSecurityAnswer3(secAnswer, accID).Equals(1)) //answer is valid
                {
                    //send email
                    return View("~/Views/Home/login.cshtml");
                }
                else //answer is not valid 
                {
                    return View("~/Views/ForgotPassword/ForgotPassword.cshtml");
                }
            }
        }
    }
}
