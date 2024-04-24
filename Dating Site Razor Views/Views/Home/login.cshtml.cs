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

namespace Dating_Site_Razor_Views.Pages
{

    public class Login : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; }


        public void OnGet()
        {

        }

        public void OnPost()
        {

        }
    }
    public class Credential
    {
        [Required(ErrorMessage = "Username Required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}