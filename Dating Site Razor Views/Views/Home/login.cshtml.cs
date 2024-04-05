using Microsoft.AspNetCore.Mvc.RazorPages;
using Dating_Site_Razor_Views.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dating_Site_Razor_Views.Pages
{
    public class Login : PageModel
    {
        [BindProperty]
        public required LoginValidation LoginValidation { get; set; }
        public void OnGet()
        {
        }

        public void OnPost() 
        { 
        
        }

    }
}
