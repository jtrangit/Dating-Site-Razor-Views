using Dating_Site_Razor_Views.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;



namespace Dating_Site_Razor_Views.Controllers
{
    public class HomeController : Controller
    {


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
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

        public IActionResult Login()
        {
            return View("~/Views/Home/login.cshtml");
        }

        public IActionResult Register()
        {
            return View("~/Views/Home/Register.cshtml");
        }

        public IActionResult Home()
        {
            return View("~/Views/Home/home.cshtml");
        }

        public IActionResult ViewProfile()
        {
            return View("~/Views/Home/viewProfile.cshtml");
        }

        public IActionResult UserProfile()
        {
            return View("~/Views/Home/userProfile.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
