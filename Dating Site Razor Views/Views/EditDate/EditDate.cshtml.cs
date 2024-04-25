using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dating_Site_Razor_Views.Views.EditDate
{
    public class EditDate : PageModel
    {

    }
    public class Date
    {
        public string partnerName { get; set; }
        public string date { get; set; }
        public string time { get; set; }

        public string description { get; set; }

        public int partnerID { get; set; }
    }
}
