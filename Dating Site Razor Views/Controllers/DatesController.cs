using Microsoft.AspNetCore.Mvc;

namespace Dating_Site_Razor_Views.Controllers
{
    public class DatesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dates()
        {
            return View("~/Views/Dates/Dates.cshtml");
        }
    }
}
