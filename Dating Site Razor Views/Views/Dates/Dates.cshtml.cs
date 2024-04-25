using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dating_Site_Razor_Views.Views.Dates
{
    public class Dates : PageModel
    {
    }

    public class DateDetails
    {
        public string datePartner { get; set; }
        public string partnerProfilePic {  get; set; }
        public int partnerID { get; set; }
        public string theDate {  get; set; }
        public string theTime { get; set; }
        public string description { get; set; }

    }
}
