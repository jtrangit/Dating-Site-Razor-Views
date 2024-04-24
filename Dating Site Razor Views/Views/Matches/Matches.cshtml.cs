using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dating_Site_Razor_Views.Views.Matches
{
    public class Matches : PageModel
    {
    }
    public class MatchedProfile
    {
        public string Name { get; set; }
        public string ProfileImg { get; set; }
        public int otherUserID { get; set; }
    }
}
