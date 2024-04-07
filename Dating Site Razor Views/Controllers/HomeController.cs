using Dating_Site_Razor_Views.Models;
using DatingSiteLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using System.Data;
using System.Diagnostics;
using Utilities;

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





        [HttpPost]
       public IActionResult AuthenticateUser()
        {
            //get the credentials that the user typed
            string username = Request.Form["Credential.Username"].ToString();
            string password = Request.Form["Credential.Password"].ToString();

            Debug.WriteLine(username);
            Debug.WriteLine(password);

            Dating login = new Dating();

            int count = login.validateLogin(username, password); //does the stored prcedure to validate username and pass

            if (count == 1)
            {
                Debug.WriteLine("Account Found");

                Dating user = new Dating();
                
                DataSet userInfo = new DataSet();

                userInfo = user.getUserInfo(username, password);

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
                

                return View("home");
            }
            else
            {
                Debug.WriteLine("Account Not Found");

                //show error message on invalid login
                string invalidCredMsg = "Username or Password is incorrect";
                ViewData["InvalidCredentials"] = invalidCredMsg;
                return View("login");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
