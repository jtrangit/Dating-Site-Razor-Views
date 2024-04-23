using Dating_Site_Razor_Views.Pages;
using DatingSiteLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Numerics;

namespace Dating_Site_Razor_Views.Controllers
{
    public class DatingHomeController : Controller
    {
        public IActionResult Home()
        {
            if (!HttpContext.Session.TryGetValue("accountID", out _))
            {
                return RedirectToAction("Login", "Home");
            }

            //States
            var stateList = new ArrayList()
            {
                "Alabama", "Alaska", "American Samoa", "Arizona", "Arkansas",
                "California", "Colorado", "Connecticut", "Delaware", "District of Columbia",
                "Federated States of Micronesia", "Florida",
                "Georgia", "Guam",
                "Hawaii",
                "Idaho", "Illinois", "Indiana", "Iowa",
                "Kansas", "Kentucky",
                "Louisiana",
                "Maine", "Marshall Islands", "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana",
                "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico", "New York", "North Carolina", "North Dakota", "Northern Mariana Islands",
                "Ohio", "Oklahoma", "Oregon",
                "Palau", "Pennsylvania", "Puerto Rico",
                "Rhode Island",
                "South Carolina", "South Dakota",
                "Tennessee", "Texas",
                "Utah",
                "Vermont", "Virgin Island", "Virginia",
                "Washington", "West Virginia", "Wisconsin", "Wyoming"
            };

            //create a select list item for each state
            List<SelectListItem> listOfStates = new List<SelectListItem>();

            foreach (string theState in stateList)
            {
                listOfStates.Add(new SelectListItem { Value = theState, Text = theState });
            }

            ViewBag.SearchStateOptions = listOfStates;

            return View("~/Views/Home/home.cshtml");
        }

        [HttpPost]
        public ActionResult ListProfiles(int? txtAgeMin, int? txtAgeMax, string ddlGender, string ddlState, string ddlCommitment)
        {
            //States
            var stateList = new ArrayList()
            {
                "Alabama", "Alaska", "American Samoa", "Arizona", "Arkansas",
                "California", "Colorado", "Connecticut", "Delaware", "District of Columbia",
                "Federated States of Micronesia", "Florida",
                "Georgia", "Guam",
                "Hawaii",
                "Idaho", "Illinois", "Indiana", "Iowa",
                "Kansas", "Kentucky",
                "Louisiana",
                "Maine", "Marshall Islands", "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana",
                "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico", "New York", "North Carolina", "North Dakota", "Northern Mariana Islands",
                "Ohio", "Oklahoma", "Oregon",
                "Palau", "Pennsylvania", "Puerto Rico",
                "Rhode Island",
                "South Carolina", "South Dakota",
                "Tennessee", "Texas",
                "Utah",
                "Vermont", "Virgin Island", "Virginia",
                "Washington", "West Virginia", "Wisconsin", "Wyoming"
            };

            //create a select list item for each state
            List<SelectListItem> listOfStates = new List<SelectListItem>();

            foreach (string theState in stateList)
            {
                listOfStates.Add(new SelectListItem { Value = theState, Text = theState });
            }

            ViewBag.SearchStateOptions = listOfStates;

            Debug.WriteLine("ListProfiles action method called.");

            // Create an instance of the Dating class
            Dating dating = new Dating();

            // Call the new stored procedure with the provided filtering criteria
            DataSet searchResults = dating.SearchProfiles(txtAgeMin, txtAgeMax, ddlGender, ddlState, ddlCommitment);

            List<Profiles> profilesList = new List<Profiles>();

            // Check if the DataSet contains any tables
            if (searchResults.Tables.Count > 0 && searchResults.Tables[0].Rows.Count > 0)
            {
                // Iterate over the search results and add them to the list
                foreach (DataRow row in searchResults.Tables[0].Rows)
                {
                    Profiles theProfile = new Profiles();
                    theProfile.profilePic = row["ProfileImg"].ToString();
                    theProfile.age = Convert.ToInt32(row["Age"]);
                    theProfile.description = row["Description"].ToString();
                    theProfile.accID = (int)row["AccountID"];
                    theProfile.city = row["City"].ToString();
                    theProfile.state = row["State"].ToString();
                    profilesList.Add(theProfile);
                }
            }
            else
            {
                // No search results found, handle this scenario (e.g., show a message)
                // You can add a message to indicate that no profiles were found
                ViewBag.Message = "No profiles found matching the criteria.";
            }

            ViewBag.ProfilesList = profilesList;

            Debug.WriteLine($"Found {profilesList.Count} profiles matching the criteria.");

            return View("~/Views/Home/home.cshtml", profilesList);
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
