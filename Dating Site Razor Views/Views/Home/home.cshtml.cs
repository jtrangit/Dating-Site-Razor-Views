using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

using Utilities;
using DatingSiteLibrary;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;
using Microsoft.IdentityModel.Tokens;

namespace Dating_Site_Razor_Views.Pages
{

    public class Home : PageModel
    {
        
        public void OnGet()
        {
            
        }
    }

    public class Profiles
    {
        public string profilePic { get; set; }
        public int age { get; set; }
        public int accID { get; set; }
        public string description { get; set; }
        public List<Profiles> profiles { get; set; }
    }
}