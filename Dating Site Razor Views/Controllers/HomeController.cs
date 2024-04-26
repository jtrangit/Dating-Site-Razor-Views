using Dating_Site_Razor_Views.Models;
using DatingSiteLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Dating_Site_Razor_Views.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Dating _dating;


        public HomeController(ILogger<HomeController> logger, Dating dating)
        {
            _logger = logger;
            _dating = dating;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    public IActionResult LandingPage()
    {
        // Fetch data from the database
        var totalUsers = _dating.GetTotalUsers();
        var totalDates = _dating.GetTotalDates();
        var totalMatches = _dating.GetTotalMatches();

        // Log the fetched data
        Debug.WriteLine($"Total Users (Before): {totalUsers}");
        Debug.WriteLine($"Total Dates (Before): {totalDates}");
        Debug.WriteLine($"Total Matches (Before): {totalMatches}");

        // Set data in ViewData
        ViewData["TotalUsers"] = totalUsers;
        ViewData["TotalDates"] = totalDates;
        ViewData["TotalMatches"] = totalMatches;

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
            if (!HttpContext.Session.TryGetValue("accountID", out _))
            {
                return RedirectToAction("Login", "Home");
            }
            return View("~/Views/Home/viewProfile.cshtml");
        }


        public IActionResult UserProfile()
        {
            if (!HttpContext.Session.TryGetValue("accountID", out _))
            {
                return RedirectToAction("Login", "Home");
            }
            return View("~/Views/Home/userProfile.cshtml");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("~/Views/Home/login.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
