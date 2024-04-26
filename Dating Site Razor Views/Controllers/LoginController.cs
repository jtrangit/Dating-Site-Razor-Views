using Dating_Site_Razor_Views.Models;
using DatingSiteLibrary;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using Utilities;

namespace Dating_Site_Razor_Views.Controllers
{
    public class LoginController : Controller
    {

        [HttpPost]
        public IActionResult AuthenticateUser()
        {

            //get the credentials that the user typed
            string username = Request.Form["Credential.Username"].ToString();
            string password = Request.Form["Credential.Password"].ToString();

            Debug.WriteLine(username);
            Debug.WriteLine(password);

            Dating login = new Dating();

            string inputPass = SecurePassword.EncryptPass(password);

            //int count = login.validateLogin(username, password); //does the stored procedure to validate username and password
            int count = login.validateLogin(username, inputPass); //does the stored procedure to validate username and password with encrypting the password the user input

            if (count == 1)
            {
                // Storing login information in a cookie if "Remember Me" is checked
                if (Request.Form["keepLoggedIn"] == "true")
                {
                    Response.Cookies.Append("username", username);
                    Response.Cookies.Append("password", password);
                }
                else
                {
                    // Delete the cookies if "Remember Me" is not checked
                    Response.Cookies.Delete("username");
                    Response.Cookies.Delete("password");
                }

                //error message is empty when the login is valid
                string invalidCredMsg = "";
                ViewData["InvalidCredentials"] = invalidCredMsg;

                Debug.WriteLine("Account Found");

                Dating user = new Dating();

                DataSet userInfo = new DataSet();

                //userInfo = user.getUserInfo(username, password); //no encryption login
                userInfo = user.getUserInfo(username, inputPass); //login with encryption

                //debug showing the values from the getUserInfo stored procedures
                Debug.WriteLine("Account ID: " + userInfo.Tables[0].Rows[0]["ID"].ToString()); //account ID#
                Debug.WriteLine("Username: " + userInfo.Tables[0].Rows[0]["Username"].ToString()); //Username
                Debug.WriteLine("Password: " + userInfo.Tables[0].Rows[0]["Password"].ToString()); //Password
                Debug.WriteLine("First Name: " + userInfo.Tables[0].Rows[0]["FirstName"].ToString()); //First Name
                Debug.WriteLine("Last Name: " + userInfo.Tables[0].Rows[0]["LastName"].ToString()); //Last Name
                Debug.WriteLine("Email: " + userInfo.Tables[0].Rows[0]["Email"].ToString()); //Email

                HttpContext.Session.SetString("accountID", userInfo.Tables[0].Rows[0]["ID"].ToString());
                Debug.WriteLine("The session accountID value is : " + HttpContext.Session.GetString("accountID"));

                HttpContext.Session.SetString("accountUsername", userInfo.Tables[0].Rows[0]["Username"].ToString());
                Debug.WriteLine("The session accountUsername value is : " + HttpContext.Session.GetString("accountUsername"));

                HttpContext.Session.SetString("accountPassword", userInfo.Tables[0].Rows[0]["Password"].ToString());
                Debug.WriteLine("The session accountPassword value is : " + HttpContext.Session.GetString("accountPassword"));

                HttpContext.Session.SetString("accountFirstName", userInfo.Tables[0].Rows[0]["FirstName"].ToString());
                Debug.WriteLine("The session accountFirstName value is : " + HttpContext.Session.GetString("accountFirstName"));

                HttpContext.Session.SetString("accountLastName", userInfo.Tables[0].Rows[0]["LastName"].ToString());
                Debug.WriteLine("The session accountLastName value is : " + HttpContext.Session.GetString("accountLastName"));

                HttpContext.Session.SetString("accountEmail", userInfo.Tables[0].Rows[0]["Email"].ToString());
                Debug.WriteLine("The session accountEmail value is : " + HttpContext.Session.GetString("accountEmail"));

                
                return RedirectToAction("Home", "DatingHome");
            }
            else
            {
                Debug.WriteLine("Account Not Found");
                
                string invalidCredMsg = "Username or Password is incorrect";
                ViewData["InvalidCredentials"] = invalidCredMsg;
                return View("~/Views/Home/login.cshtml");
            }
        }

    }
}