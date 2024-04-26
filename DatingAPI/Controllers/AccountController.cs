using Microsoft.AspNetCore.Mvc;
using Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using DatingSiteLibrary;

namespace DatingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost("securityquestion")]
        public IActionResult SecurityQuestion(string email)
        {
            // Check if the email is valid
            Dating checkEmail = new Dating();
            int isValidEmail = checkEmail.getValidEmail(email);

            if (isValidEmail == 1) // Valid email
            {
                // Generate a security question
                List<string> securityQuestions = new List<string>
                {
                    "What was your first pet's name?",
                    "What was your mother's maiden name?",
                    "What is your favorite food?"
                };

                Random random = new Random();
                string securityQuestion = securityQuestions[random.Next(0, securityQuestions.Count)];

                // Store the security question and email in TempData or return them in the response
                return Ok(new { SecurityQuestion = securityQuestion, Email = email });
            }
            else // Invalid email
            {
                return BadRequest("Email does not belong to an account");
            }
        }

        [HttpPost("sendresetemail")]
        public IActionResult SendResetEmail(string email, string secQuestion, string secAnswer)
        {
            // Get the account ID of the account with the Email
            Dating getAccID = new Dating();
            DataSet ds = getAccID.getLoginFromEmail(email);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                int accID = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"]);

                // Check the security answer based on the question
                Dating checkAnswer = new Dating();
                int isValidAnswer = -1;

                switch (secQuestion)
                {
                    case "What was your first pet's name?":
                        isValidAnswer = checkAnswer.getSecurityAnswer1(secAnswer, accID);
                        break;
                    case "What was your mother's maiden name?":
                        isValidAnswer = checkAnswer.getSecurityAnswer2(secAnswer, accID);
                        break;
                    case "What is your favorite food?":
                        isValidAnswer = checkAnswer.getSecurityAnswer3(secAnswer, accID);
                        break;
                }

                if (isValidAnswer == 1) // Valid answer
                {
                    // Send the reset email
                    var emailSender = new Email();
                    emailSender.Recipient = email;
                    emailSender.Sender = "smtp@temple.edu"; // Set your email address here
                    emailSender.Subject = "Password Reset";
                    emailSender.Message = "Please follow the instructions in this email to reset your password.";

                    try
                    {
                        emailSender.SendMail();
                        return Ok("Password reset email sent successfully.");
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Error sending reset email: {ex.Message}");
                    }
                }
                else // Invalid answer
                {
                    return BadRequest("Invalid security answer.");
                }
            }
            else // No account found for the provided email
            {
                return BadRequest("No account found for the provided email.");
            }
        }
    }
}