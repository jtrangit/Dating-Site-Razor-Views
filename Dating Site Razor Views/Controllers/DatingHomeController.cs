using Dating_Site_Razor_Views.Pages;
using DatingSiteLibrary;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;

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
                profilesList.Add(theProfile);
            }

            profiles.profiles = profilesList;

            ViewBag.ProfilesList = profilesList;

            return View("~/Views/Home/home.cshtml", profiles);
        }

        public IActionResult ViewSpecificProfile(string accID)
        {
            //int accID = Convert.ToInt32(Request.Form["AccountID"]);
            // Debug.WriteLine("accID: " + accID);
            string theID = accID;
            Debug.WriteLine(theID);

            return View("~/Views/Home/viewProfile.cshtml");
        }
    }
}
