using Dating_Site_Razor_Views.Pages;
using DatingSiteLibrary;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System.Numerics;

namespace Dating_Site_Razor_Views.Controllers
{
    public class DatingHomeController : Controller
    {
        [HttpPost]
        public ActionResult ListProfiles()
        {

            var filterType = Request.Form["ddlFilterProfiles"].ToString();
            Debug.WriteLine(filterType);

            int UserAccID = Convert.ToInt32(HttpContext.Session.GetString("accountID"));
            Debug.WriteLine(UserAccID.ToString());

            Dating User = new Dating();
            DataSet UserInfo = new DataSet();

            UserInfo = User.getProfileInfo(UserAccID);

            int userAge = (int)UserInfo.Tables[0].Rows[0]["Age"];
            string userGender = (string)UserInfo.Tables[0].Rows[0]["Gender"];
            string userCommitment = (string)UserInfo.Tables[0].Rows[0]["Commitment"];
            string userState = (string)UserInfo.Tables[0].Rows[0]["State"];

            DataSet otherProfiles = new DataSet();

            if (filterType == "Age")
            {
                Dating otherUsers = new Dating();
                otherProfiles = otherUsers.getProfilesByAge(UserAccID, userAge - 2, userAge + 2);
            }
            else if (filterType == "Gender")
            {
                Dating otherUsers = new Dating();
                otherProfiles = otherUsers.getProfilesByGender(UserAccID, userGender);
            }
            else if (filterType == "State")
            {
                Dating otherUsers = new Dating();
                otherProfiles = otherUsers.getProfilesByState(UserAccID, userState);
            }
            else //filter type commitment
            {
                Dating otherUsers = new Dating();
                otherProfiles = otherUsers.getProfilesByCommitment(UserAccID, userCommitment);
            }

            Profiles profiles = new Profiles();

            List<Profiles> profilesList = new List<Profiles>();

            //add every profile into list to be shown
            for (int i = 0; i < otherProfiles.Tables[0].Rows.Count; i++)
            {
                Profiles theProfile = new Profiles();
                theProfile.profilePic = otherProfiles.Tables[0].Rows[i]["ProfileImg"].ToString();
                theProfile.age = Convert.ToInt32(otherProfiles.Tables[0].Rows[i]["Age"]);
                theProfile.description = otherProfiles.Tables[0].Rows[i]["Description"].ToString();
                theProfile.accID = (int)otherProfiles.Tables[0].Rows[i]["AccountID"];
                theProfile.city = otherProfiles.Tables[0].Rows[i]["City"].ToString();
                theProfile.state = otherProfiles.Tables[0].Rows[i]["State"].ToString();
                profilesList.Add(theProfile);
            }

            profiles.profiles = profilesList;

            ViewBag.ProfilesList = profilesList;

            return View("~/Views/Home/home.cshtml", profiles);
        }

        public IActionResult ViewSpecificProfile(string accID)
        {
            int profID = Convert.ToInt32(accID);

            Dating dating = new Dating();

            DataSet theProfile = new DataSet();

            theProfile = dating.getProfileInfo(profID);
            string fullname = (string)theProfile.Tables[0].Rows[0]["Name"];
            string[] name;

            name = fullname.Split(' ');

            string firstName = name[0];
            string lastName = name[1];
            string profilePic = (string)theProfile.Tables[0].Rows[0]["ProfileImg"];
            string city = (string)theProfile.Tables[0].Rows[0]["City"];
            string state = (string)theProfile.Tables[0].Rows[0]["State"];
            string gender = (string)theProfile.Tables[0].Rows[0]["Gender"];
            string height = (string)theProfile.Tables[0].Rows[0]["Height"];
            decimal weight = (decimal)theProfile.Tables[0].Rows[0]["Weight"];
            int age = Convert.ToInt32(theProfile.Tables[0].Rows[0]["Age"]);
            string occupation = (string)theProfile.Tables[0].Rows[0]["Occupation"];
            string commitment = (string)theProfile.Tables[0].Rows[0]["Commitment"];
            string description = (string)theProfile.Tables[0].Rows[0]["Description"];
            
            ViewedProfile profile = new ViewedProfile();
            profile.FirstName = firstName;
            profile.LastName = lastName;
            profile.profilePic = profilePic;
            profile.City = city;
            profile.State = state;
            profile.Gender = gender;
            profile.Height = height;
            profile.Weight = weight.ToString();
            profile.Age = age;
            profile.Occupation = occupation;
            profile.Commitment = commitment;
            profile.Description = description;

            //private info that will be hidden until the two profiles have a date
            string email = (string)theProfile.Tables[0].Rows[0]["Email"];
            string address = (string)theProfile.Tables[0].Rows[0]["Address"];
            string phoneNumber = (string)theProfile.Tables[0].Rows[0]["PhoneNumber"];

            //checks to see if the profiles have a date so that private data can be shown, if not [PRIVATE] is shown in place instead
            Dating check = new Dating();
            int userID = Convert.ToInt32(HttpContext.Session.GetString("accountID"));
            if(check.getValidDate(userID, profID).Equals(1))
            {
                //private info
                profile.Email = email;
                profile.Address = address;
                profile.PhoneNumber = phoneNumber;
            }
            else
            {
                profile.Email = "[PRIVATE]";
                profile.Address = "[PRIVATE]";
                profile.PhoneNumber = "[PRIVATE]";
            }

            List<ViewedProfile> viewedProf = new List<ViewedProfile>();
            viewedProf.Add(profile);

            profile.TheProfile = viewedProf;

            ViewBag.ProfileBeingViewed = viewedProf;

            return View("~/Views/Home/viewProfile.cshtml", profile);
        }
    }
}
