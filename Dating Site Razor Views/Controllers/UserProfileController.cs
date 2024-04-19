using Microsoft.AspNetCore.Mvc;

namespace Dating_Site_Razor_Views.Controllers
{
    public class UserProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
