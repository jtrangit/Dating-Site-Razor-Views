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
    public class UserProfile : PageModel
    {

    }
    public class EditProfile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Gender { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public int Age { get; set; }
        public string Occupation { get; set; }
        public string Commitment { get; set; }
        public string Description { get; set; }
        public string profilePic { get; set; }
        public string Visible { get; set; }

        public List<EditProfile> EditProf { get; set; }

    }

    public class UserInterests
    {
        public string name;

        public bool isChecked; 
    }
}