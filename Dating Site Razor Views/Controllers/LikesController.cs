using Microsoft.AspNetCore.Mvc;

namespace Dating_Site_Razor_Views.Controllers
{
    public class LikesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Likes()
        {
            return View("~/Views/Likes/Likes.cshtml");
        }
    }
}
