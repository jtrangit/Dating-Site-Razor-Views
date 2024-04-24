using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dating_Site_Razor_Views.Views.Likes
{
    public class Likes : PageModel
    {

    }
    public class likedProfile
    {
        public string name { get; set; }
        public string profilePic { get; set; }
        public string status { get; set; }

        public string initiator {  get; set; }

    }
}
