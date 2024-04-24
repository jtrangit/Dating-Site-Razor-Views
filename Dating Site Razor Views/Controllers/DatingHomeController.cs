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
            Debug.WriteLine("ListProfiles action method called.");

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

            Dating dating2 = new Dating();
            DataSet profileQuestions = new DataSet();
            profileQuestions = dating2.getProfileQuestions(profID);

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
            string q1 = (string)profileQuestions.Tables[0].Rows[0]["Question1"];
            string q2 = (string)profileQuestions.Tables[0].Rows[0]["Question2"];
            string q3 = (string)profileQuestions.Tables[0].Rows[0]["Question3"];

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
            profile.Question1 = q1;
            profile.Question2 = q2;
            profile.Question3 = q3;
            profile.accountID = profID;

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

            //Photo gallery
            Dating dating3 = new Dating();
            DataSet photos = new DataSet();

            photos = dating3.getPhotoGallery(profID);

            List<string> viewPhotos = new List<string>();

            if (photos != null)
            {
                foreach (DataTable dataTable in photos.Tables)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (row["PhotoUrl"] != DBNull.Value || row["PhotoUrl"] != null)
                            viewPhotos.Add(row["PhotoUrl"].ToString());
                    }
                }
            }
            else
            {
                viewPhotos = null;
            }

            ViewBag.ViewPhotoGallery = viewPhotos;

            //get the profile's interests
            //Get the interests
            Dating userInterests = new Dating();
            DataSet ds = new DataSet();
            List<string> interests = new List<string>();

            ds = userInterests.getInterests(profID);

            //null logic for the table, if null default to 0 so the empty user interests stays empty
            if (ds.Tables[0].Rows[0]["Movies"] == DBNull.Value)
            {
                //do nothing
            }
            else if (ds.Tables[0].Rows[0]["Movies"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                //do nothing
            }
            else //BIT value of 1 is true 
            {
                interests.Add("Movies");
            }

            if (ds.Tables[0].Rows[0]["TV"] == DBNull.Value)
            {
                //do nothing
            }
            else if (ds.Tables[0].Rows[0]["TV"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                //do nothing
            }
            else //BIT value of 1 is true 
            {
                interests.Add("TV");
            }
            ////////////////////////////////////////////////////////////
            if (ds.Tables[0].Rows[0]["Anime"] == DBNull.Value)
            {
                //do nothing
            }
            else if (ds.Tables[0].Rows[0]["Anime"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                //do nothing
            }
            else //BIT value of 1 is true 
            {
                interests.Add("Anime");
            }

            if (ds.Tables[0].Rows[0]["Manga"] == DBNull.Value)
            {
                //do nothing
            }
            else if (ds.Tables[0].Rows[0]["Manga"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                //do nothing
            }
            else //BIT value of 1 is true 
            {
                interests.Add("Manga");
            }

            if (ds.Tables[0].Rows[0]["Books"] == DBNull.Value)
            {
                //do nothing
            }
            else if (ds.Tables[0].Rows[0]["Books"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                //do nothing
            }
            else //BIT value of 1 is true 
            {
                interests.Add("Books");
            }

            if (ds.Tables[0].Rows[0]["Video Games"] == DBNull.Value)
            {
                //do nothing
            }
            else if (ds.Tables[0].Rows[0]["Video Games"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                //do nothing
            }
            else //BIT value of 1 is true 
            {
                interests.Add("Video Games");
            }

            if (ds.Tables[0].Rows[0]["Sports"] == DBNull.Value)
            {
                //do nothing
            }
            else if (ds.Tables[0].Rows[0]["Sports"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                //do nothing
            }
            else //BIT value of 1 is true 
            {
                interests.Add("Sports");
            }

            if (ds.Tables[0].Rows[0]["Gym"] == DBNull.Value)
            {
                //do nothing
            }
            else if (ds.Tables[0].Rows[0]["Gym"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                //do nothing
            }
            else //BIT value of 1 is true 
            {
                interests.Add("Gym");
            }

            if (ds.Tables[0].Rows[0]["Cooking"] == DBNull.Value)
            {
                //do nothing
            }
            else if (ds.Tables[0].Rows[0]["Cooking"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                //do nothing
            }
            else //BIT value of 1 is true 
            {
                interests.Add("Cooking");
            }

            if (ds.Tables[0].Rows[0]["Martial Arts"] == DBNull.Value)
            {
                //do nothing;
            }
            else if (ds.Tables[0].Rows[0]["Martial Arts"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                //do nothing
            }
            else //BIT value of 1 is true 
            {
                interests.Add("Martial Arts");
            }

            if (ds.Tables[0].Rows[0]["Art"] == DBNull.Value)
            {
                //do nothing
            }
            else if (ds.Tables[0].Rows[0]["Art"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                //do nothing
            }
            else //BIT value of 1 is true 
            {
                interests.Add("Art");
            }

            if (ds.Tables[0].Rows[0]["Hiking"] == DBNull.Value)
            {
                //do nothing
            }
            else if (ds.Tables[0].Rows[0]["Hiking"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                //do nothing
            }
            else //BIT value of 1 is true 
            {
                interests.Add("Hiking");
            }

            if (ds.Tables[0].Rows[0]["Partying"] == DBNull.Value)
            {
                //do nothing
            }
            else if (ds.Tables[0].Rows[0]["Partying"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                //do nothing
            }
            else //BIT value of 1 is true 
            {
                interests.Add("Partying");
            }

            if (ds.Tables[0].Rows[0]["Music"] == DBNull.Value)
            {
                //do nothing
            }
            else if (ds.Tables[0].Rows[0]["Music"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                //do nothing
            }
            else //BIT value of 1 is true 
            {
                interests.Add("Music");
            }

            if (ds.Tables[0].Rows[0]["Dancing"] == DBNull.Value)
            {
                //do nothing
            }
            else if (ds.Tables[0].Rows[0]["Dancing"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                //do nothing
            }
            else //BIT value of 1 is true 
            {
                interests.Add("Dancing");
            }

            if (ds.Tables[0].Rows[0]["Other"] == DBNull.Value || ds.Tables[0].Rows[0]["Other"].Equals(""))
            {
                //do nothing
                //userOtherInterest = "";
            }
            else
            {
                interests.Add(ds.Tables[0].Rows[0]["Other"].ToString());
            }

            ViewBag.UserInterests = interests;

            //Get Dislikes
            Dating userDislikes = new Dating();
            DataSet ds2 = new DataSet();
            List<string> dislikes = new List<string>();

            ds2 = userDislikes.getDatingDislikes(profID);

            //null logic for the table, if null default to 0 so the empty user interests stays empty
            if (ds2.Tables[0].Rows[0]["Movies"] == DBNull.Value)
            {
                //do nothing
            }
            else if (ds2.Tables[0].Rows[0]["Movies"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                //do nothing
            }
            else //BIT value of 1 is true 
            {
                dislikes.Add("Movies");
            }

            if (ds2.Tables[0].Rows[0]["TV"] == DBNull.Value)
            {
                //do nothing
            }
            else if (ds2.Tables[0].Rows[0]["TV"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                //do nothing
            }
            else //BIT value of 1 is true 
            {
                dislikes.Add("TV");
            }
            ////////////////////////////////////////////////////////////
            if (ds2.Tables[0].Rows[0]["Anime"] == DBNull.Value)
            {
                //do nothing
            }
            else if (ds2.Tables[0].Rows[0]["Anime"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                //do nothing
            }
            else //BIT value of 1 is true 
            {
                dislikes.Add("Anime");
            }

            if (ds2.Tables[0].Rows[0]["Manga"] == DBNull.Value)
            {
                //do nothing
            }
            else if (ds2.Tables[0].Rows[0]["Manga"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                //do nothing
            }
            else //BIT value of 1 is true 
            {
                dislikes.Add("Manga");
            }

            if (ds2.Tables[0].Rows[0]["Books"] == DBNull.Value)
            {
                //do nothing
            }
            else if (ds2.Tables[0].Rows[0]["Books"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                //do nothing
            }
            else //BIT value of 1 is true 
            {
                dislikes.Add("Books");
            }

            if (ds.Tables[0].Rows[0]["Video Games"] == DBNull.Value)
            {
                //do nothing
            }
            else if (ds2.Tables[0].Rows[0]["Video Games"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                //do nothing
            }
            else //BIT value of 1 is true 
            {
                dislikes.Add("Video Games");
            }

            if (ds2.Tables[0].Rows[0]["Sports"] == DBNull.Value)
            {
                //do nothing
            }
            else if (ds2.Tables[0].Rows[0]["Sports"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                //do nothing
            }
            else //BIT value of 1 is true 
            {
                dislikes.Add("Sports");
            }

            if (ds2.Tables[0].Rows[0]["Gym"] == DBNull.Value)
            {
                //do nothing
            }
            else if (ds2.Tables[0].Rows[0]["Gym"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                //do nothing
            }
            else //BIT value of 1 is true 
            {
                dislikes.Add("Gym");
            }

            if (ds2.Tables[0].Rows[0]["Cooking"] == DBNull.Value)
            {
                //do nothing
            }
            else if (ds2.Tables[0].Rows[0]["Cooking"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                //do nothing
            }
            else //BIT value of 1 is true 
            {
                dislikes.Add("Cooking");
            }

            if (ds2.Tables[0].Rows[0]["Martial Arts"] == DBNull.Value)
            {
                //do nothing;
            }
            else if (ds2.Tables[0].Rows[0]["Martial Arts"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                //do nothing
            }
            else //BIT value of 1 is true 
            {
                dislikes.Add("Martial Arts");
            }

            if (ds2.Tables[0].Rows[0]["Art"] == DBNull.Value)
            {
                //do nothing
            }
            else if (ds2.Tables[0].Rows[0]["Art"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                //do nothing
            }
            else //BIT value of 1 is true 
            {
                dislikes.Add("Art");
            }

            if (ds2.Tables[0].Rows[0]["Hiking"] == DBNull.Value)
            {
                //do nothing
            }
            else if (ds2.Tables[0].Rows[0]["Hiking"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                //do nothing
            }
            else //BIT value of 1 is true 
            {
                dislikes.Add("Hiking");
            }

            if (ds2.Tables[0].Rows[0]["Partying"] == DBNull.Value)
            {
                //do nothing
            }
            else if (ds2.Tables[0].Rows[0]["Partying"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                //do nothing
            }
            else //BIT value of 1 is true 
            {
                dislikes.Add("Partying");
            }

            if (ds2.Tables[0].Rows[0]["Music"] == DBNull.Value)
            {
                //do nothing
            }
            else if (ds2.Tables[0].Rows[0]["Music"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                //do nothing
            }
            else //BIT value of 1 is true 
            {
                dislikes.Add("Music");
            }

            if (ds2.Tables[0].Rows[0]["Dancing"] == DBNull.Value)
            {
                //do nothing
            }
            else if (ds2.Tables[0].Rows[0]["Dancing"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                //do nothing
            }
            else //BIT value of 1 is true 
            {
                dislikes.Add("Dancing");
            }

            if (ds2.Tables[0].Rows[0]["Other"] == DBNull.Value || ds2.Tables[0].Rows[0]["Other"].Equals(""))
            {
                //do nothing
                //userOtherInterest = "";
            }
            else
            {
                dislikes.Add(ds2.Tables[0].Rows[0]["Other"].ToString());
            }

            ViewBag.UserDislikes = dislikes;

            return View("~/Views/Home/viewProfile.cshtml", profile);
        }
        public IActionResult UserProfile()
        {
            if (!HttpContext.Session.TryGetValue("accountID", out _))
            {
                return RedirectToAction("Login", "Home");
            }
            int accID = Convert.ToInt32(HttpContext.Session.GetString("accountID"));

            Dating dating = new Dating();
            DataSet userInfo = new DataSet();

            userInfo = dating.getProfileInfo(accID);

            //split the full name of the user to first and last name
            string fullname = (string)userInfo.Tables[0].Rows[0]["Name"];
            string[] name;

            name = fullname.Split(' ');

            string firstName = name[0];
            string lastName = name[1];


            string profilePic;
            int age;
            decimal weight;

            if (userInfo.Tables[0].Rows[0]["Age"] == DBNull.Value)
            {
                age = 18;
            }
            else
            {
                age = Convert.ToInt32(userInfo.Tables[0].Rows[0]["Age"]);
            }

            if (userInfo.Tables[0].Rows[0]["Weight"] == DBNull.Value)
            {
                weight = 155;
            }
            else
            {
                weight = Convert.ToDecimal(userInfo.Tables[0].Rows[0]["Weight"]);
            }

            if (userInfo.Tables[0].Rows[0]["ProfileImg"] == DBNull.Value)
            {
                profilePic = "https://upload.wikimedia.org/wikipedia/commons/5/5a/Black_question_mark.png";
            }
            else
            {
                profilePic = userInfo.Tables[0].Rows[0]["ProfileImg"].ToString();
            }

            ViewBag.UserProfilePic = profilePic;

            string city;
            string state;
            string gender;
            string height;
            string occupation;
            string commitment;
            string description;
            //private info 
            string email;
            string address;
            string phoneNumber;

            if (userInfo.Tables[0].Rows[0]["City"] == DBNull.Value)
            {
                city = "No City Saved";
            }
            else
            {
                city = (string)userInfo.Tables[0].Rows[0]["City"];
            }

            if (userInfo.Tables[0].Rows[0]["State"] == DBNull.Value)
            {
                state = "Alabama";
            }
            else
            {
                state = (string)userInfo.Tables[0].Rows[0]["State"];
            }

            if (userInfo.Tables[0].Rows[0]["Gender"] == DBNull.Value)
            {
                gender = "Male";
            }
            else
            {
                gender = (string)userInfo.Tables[0].Rows[0]["Gender"];
            }

            if (userInfo.Tables[0].Rows[0]["PhoneNumber"] == DBNull.Value)
            {
                phoneNumber = "No Phone Number Saved";
            }
            else
            {
                phoneNumber = (string)userInfo.Tables[0].Rows[0]["PhoneNumber"];
            }

            if (userInfo.Tables[0].Rows[0]["Email"] == DBNull.Value)
            {
                email = "No Email Saved";
            }
            else
            {
                email = (string)userInfo.Tables[0].Rows[0]["Email"];
            }

            if (userInfo.Tables[0].Rows[0]["Address"] == DBNull.Value)
            {
                address = "No Address Saved";
            }
            else
            {
                address = (string)userInfo.Tables[0].Rows[0]["Address"];
            }

            if (userInfo.Tables[0].Rows[0]["Height"] == DBNull.Value)
            {
                height = "No Height Saved";
            }
            else
            {
                height = (string)userInfo.Tables[0].Rows[0]["Height"];
            }

            if (userInfo.Tables[0].Rows[0]["Occupation"] == DBNull.Value)
            {
                occupation = "No Occupation Saved";
            }
            else
            {
                occupation = (string)userInfo.Tables[0].Rows[0]["Occupation"];
            }

            if (userInfo.Tables[0].Rows[0]["Occupation"] == DBNull.Value)
            {
                commitment = "Platonic";
            }
            else
            {
                commitment = (string)userInfo.Tables[0].Rows[0]["Commitment"];
            }

            if (userInfo.Tables[0].Rows[0]["Description"] == DBNull.Value)
            {
                description = "No Description";
            }
            else
            {
                description = (string)userInfo.Tables[0].Rows[0]["Description"];
            }

            bool accVisible;
            string visible;

            if (userInfo.Tables[0].Rows[0]["Visible"] == DBNull.Value)
            {
                accVisible = true;
                visible = "Visible";
            }
            else if (userInfo.Tables[0].Rows[0]["Visible"].Equals(true))
            {
                accVisible = true;
                visible = "Visible";
            }
            else
            {
                accVisible = false;
                visible = "Not Visible";
            }

            //get the profile questions
            Dating profQuestions = new Dating();
            DataSet theQuestions = new DataSet();

            theQuestions = profQuestions.getProfileQuestions(accID);

            string q1;
            string q2;
            string q3;

            if (theQuestions.Tables[0].Rows[0]["Question1"] == DBNull.Value)
            {
                q1 = "Not saved";
            }
            else if (theQuestions.Tables[0].Rows[0]["Question1"].Equals(null))
            {
                q1 = "Not saved";
            }
            else
            {
                q1 = (string)theQuestions.Tables[0].Rows[0]["Question1"];
            }
            if (theQuestions.Tables[0].Rows[0]["Question2"] == DBNull.Value)
            {
                q2 = "Not saved";
            }
            else if (theQuestions.Tables[0].Rows[0]["Question2"].Equals(null))
            {
                q2 = "Not saved";
            }
            else
            {
                q2 = theQuestions.Tables[0].Rows[0]["Question2"].ToString();
            }
            if (theQuestions.Tables[0].Rows[0]["Question3"] == DBNull.Value)
            {
                q3 = "Not saved";
            }
            else if (theQuestions.Tables[0].Rows[0]["Question3"].Equals(null))
            {
                q3 = "Not saved";
            }
            else
            {
                q3 = theQuestions.Tables[0].Rows[0]["Question3"].ToString();
            }

            EditProfile editProfile = new EditProfile();
            editProfile.FirstName = firstName;
            editProfile.LastName = lastName;
            editProfile.profilePic = profilePic;
            editProfile.City = city;
            editProfile.State = state;
            editProfile.Gender = gender;
            editProfile.Height = height;
            editProfile.Weight = weight.ToString();
            editProfile.Age = age;
            editProfile.Occupation = occupation;
            editProfile.Commitment = commitment;
            editProfile.Description = description;
            editProfile.Visible = visible;
            editProfile.PhoneNumber = phoneNumber;
            editProfile.Email = email;
            editProfile.Address = address;
            editProfile.Question1 = q1;
            editProfile.Question2 = q2;
            editProfile.Question3 = q3;

            List<EditProfile> theProfile = new List<EditProfile>();
            theProfile.Add(editProfile);

            editProfile.EditProf = theProfile;

            ViewBag.LoggedInUserProfile = theProfile;

            //populate the visibility dropdownbox
            List<SelectListItem> visibleOptions = new List<SelectListItem>();

            if (accVisible == true)
            {
                visibleOptions.Add(new SelectListItem { Text = "Yes", Value = "true", Selected = true });
                visibleOptions.Add(new SelectListItem { Text = "No", Value = "false" });
            }
            else
            {
                visibleOptions.Add(new SelectListItem { Text = "Yes", Value = "true" });
                visibleOptions.Add(new SelectListItem { Text = "No", Value = "false", Selected = true });
            }
            ViewBag.VisibleOptions = visibleOptions;

            //populate dropdownbox for gender
            List<SelectListItem> genderOptions = new List<SelectListItem>();

            if (gender == "Male")
            {
                genderOptions.Add(new SelectListItem { Text = "Male", Value = "Male", Selected = true });
                genderOptions.Add(new SelectListItem { Text = "Female", Value = "Female" });
                genderOptions.Add(new SelectListItem { Text = "Non-binary", Value = "Non-binary" });
                genderOptions.Add(new SelectListItem { Text = "Other", Value = "Other" });
            }
            else if (gender == "Female")
            {
                genderOptions.Add(new SelectListItem { Text = "Male", Value = "Male" });
                genderOptions.Add(new SelectListItem { Text = "Female", Value = "Female", Selected = true });
                genderOptions.Add(new SelectListItem { Text = "Non-binary", Value = "Non-binary" });
                genderOptions.Add(new SelectListItem { Text = "Other", Value = "Other" });

            }
            else if (gender == "Non-binary")
            {
                genderOptions.Add(new SelectListItem { Text = "Male", Value = "Male" });
                genderOptions.Add(new SelectListItem { Text = "Female", Value = "Female" });
                genderOptions.Add(new SelectListItem { Text = "Non-binary", Value = "Non-binary", Selected = true });
                genderOptions.Add(new SelectListItem { Text = "Other", Value = "Other" });
            }
            else
            {
                genderOptions.Add(new SelectListItem { Text = "Male", Value = "Male" });
                genderOptions.Add(new SelectListItem { Text = "Female", Value = "Female" });
                genderOptions.Add(new SelectListItem { Text = "Non-binary", Value = "Non-binary" });
                genderOptions.Add(new SelectListItem { Text = "Other", Value = "Other", Selected = true });
            }
            ViewBag.genderOptions = genderOptions;

            //populate commitment dropdownbox
            List<SelectListItem> commitmentOptions = new List<SelectListItem>();

            if (commitment == "Platonic")
            {
                commitmentOptions.Add(new SelectListItem { Text = "Platonic", Value = "Platonic", Selected = true });
                commitmentOptions.Add(new SelectListItem { Text = "Relationship", Value = "Relationship" });
                commitmentOptions.Add(new SelectListItem { Text = "Marriage", Value = "Marriage" });
            }
            else if (commitment == "Relationship")
            {
                commitmentOptions.Add(new SelectListItem { Text = "Platonic", Value = "Platonic" });
                commitmentOptions.Add(new SelectListItem { Text = "Relationship", Value = "Relationship", Selected = true });
                commitmentOptions.Add(new SelectListItem { Text = "Marriage", Value = "Marriage" });
            }
            else
            {
                commitmentOptions.Add(new SelectListItem { Text = "Platonic", Value = "Platonic" });
                commitmentOptions.Add(new SelectListItem { Text = "Relationship", Value = "Relationship" });
                commitmentOptions.Add(new SelectListItem { Text = "Marriage", Value = "Marriage", Selected = true });
            }
            ViewBag.CommitmentOptions = commitmentOptions;

            //Get the interests
            Dating userInterests = new Dating();
            DataSet ds = new DataSet();
            List<UserInterests> interests = new List<UserInterests>();

            ds = userInterests.getInterests(accID);

            string userOtherInterest;

            //null logic for the table, if null default to 0 so the empty user interests stays empty
            if (ds.Tables[0].Rows[0]["Movies"] == DBNull.Value)
            {
                interests.Add(new UserInterests { name = "Movies", isChecked = false });
            }
            else if (ds.Tables[0].Rows[0]["Movies"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                interests.Add(new UserInterests { name = "Movies", isChecked = false });
            }
            else //BIT value of 1 is true 
            {
                interests.Add(new UserInterests { name = "Movies", isChecked = true });
            }

            if (ds.Tables[0].Rows[0]["TV"] == DBNull.Value)
            {
                interests.Add(new UserInterests { name = "TV", isChecked = false });
            }
            else if (ds.Tables[0].Rows[0]["TV"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                interests.Add(new UserInterests { name = "TV", isChecked = false });
            }
            else //BIT value of 1 is true 
            {
                interests.Add(new UserInterests { name = "TV", isChecked = true });
            }
            ////////////////////////////////////////////////////////////
            if (ds.Tables[0].Rows[0]["Anime"] == DBNull.Value)
            {
                interests.Add(new UserInterests { name = "Anime", isChecked = false });
            }
            else if (ds.Tables[0].Rows[0]["Anime"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                interests.Add(new UserInterests { name = "Anime", isChecked = false });
            }
            else //BIT value of 1 is true 
            {
                interests.Add(new UserInterests { name = "Anime", isChecked = false });
            }

            if (ds.Tables[0].Rows[0]["Manga"] == DBNull.Value)
            {
                interests.Add(new UserInterests { name = "Manga", isChecked = false });
            }
            else if (ds.Tables[0].Rows[0]["Manga"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                interests.Add(new UserInterests { name = "Manga", isChecked = false });
            }
            else //BIT value of 1 is true 
            {
                interests.Add(new UserInterests { name = "Manga", isChecked = false });
            }

            if (ds.Tables[0].Rows[0]["Books"] == DBNull.Value)
            {
                interests.Add(new UserInterests { name = "Books", isChecked = false });
            }
            else if (ds.Tables[0].Rows[0]["Books"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                interests.Add(new UserInterests { name = "Books", isChecked = false });
            }
            else //BIT value of 1 is true 
            {
                interests.Add(new UserInterests { name = "Books", isChecked = false });
            }

            if (ds.Tables[0].Rows[0]["Video Games"] == DBNull.Value)
            {
                interests.Add(new UserInterests { name = "Video Games", isChecked = false });
            }
            else if (ds.Tables[0].Rows[0]["Video Games"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                interests.Add(new UserInterests { name = "Video Games", isChecked = false });
            }
            else //BIT value of 1 is true 
            {
                interests.Add(new UserInterests { name = "Video Games", isChecked = false });
            }

            if (ds.Tables[0].Rows[0]["Sports"] == DBNull.Value)
            {
                interests.Add(new UserInterests { name = "Sports", isChecked = false });
            }
            else if (ds.Tables[0].Rows[0]["Sports"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                interests.Add(new UserInterests { name = "Sports", isChecked = false });
            }
            else //BIT value of 1 is true 
            {
                interests.Add(new UserInterests { name = "Sports", isChecked = false });
            }

            if (ds.Tables[0].Rows[0]["Gym"] == DBNull.Value)
            {
                interests.Add(new UserInterests { name = "Gym", isChecked = false });
            }
            else if (ds.Tables[0].Rows[0]["Gym"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                interests.Add(new UserInterests { name = "Gym", isChecked = false });
            }
            else //BIT value of 1 is true 
            {
                interests.Add(new UserInterests { name = "Gym", isChecked = false });
            }

            if (ds.Tables[0].Rows[0]["Cooking"] == DBNull.Value)
            {
                interests.Add(new UserInterests { name = "Cooking", isChecked = false });
            }
            else if (ds.Tables[0].Rows[0]["Cooking"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                interests.Add(new UserInterests { name = "Cooking", isChecked = false });
            }
            else //BIT value of 1 is true 
            {
                interests.Add(new UserInterests { name = "Cooking", isChecked = false });
            }

            if (ds.Tables[0].Rows[0]["Martial Arts"] == DBNull.Value)
            {
                interests.Add(new UserInterests { name = "Martial Arts", isChecked = false });
            }
            else if (ds.Tables[0].Rows[0]["Martial Arts"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                interests.Add(new UserInterests { name = "Martial Arts", isChecked = false });
            }
            else //BIT value of 1 is true 
            {
                interests.Add(new UserInterests { name = "Martial Arts", isChecked = false });
            }

            if (ds.Tables[0].Rows[0]["Art"] == DBNull.Value)
            {
                interests.Add(new UserInterests { name = "Art", isChecked = false });
            }
            else if (ds.Tables[0].Rows[0]["Art"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                interests.Add(new UserInterests { name = "Art", isChecked = false });
            }
            else //BIT value of 1 is true 
            {
                interests.Add(new UserInterests { name = "Art", isChecked = false });
            }

            if (ds.Tables[0].Rows[0]["Hiking"] == DBNull.Value)
            {
                interests.Add(new UserInterests { name = "Hiking", isChecked = false });
            }
            else if (ds.Tables[0].Rows[0]["Hiking"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                interests.Add(new UserInterests { name = "Hiking", isChecked = false });
            }
            else //BIT value of 1 is true 
            {
                interests.Add(new UserInterests { name = "Hiking", isChecked = false });
            }

            if (ds.Tables[0].Rows[0]["Partying"] == DBNull.Value)
            {
                interests.Add(new UserInterests { name = "Partying", isChecked = false });
            }
            else if (ds.Tables[0].Rows[0]["Partying"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                interests.Add(new UserInterests { name = "Partying", isChecked = false });
            }
            else //BIT value of 1 is true 
            {
                interests.Add(new UserInterests { name = "Partying", isChecked = false });
            }

            if (ds.Tables[0].Rows[0]["Music"] == DBNull.Value)
            {
                interests.Add(new UserInterests { name = "Music", isChecked = false });
            }
            else if (ds.Tables[0].Rows[0]["Music"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                interests.Add(new UserInterests { name = "Music", isChecked = false });
            }
            else //BIT value of 1 is true 
            {
                interests.Add(new UserInterests { name = "Music", isChecked = false });
            }

            if (ds.Tables[0].Rows[0]["Dancing"] == DBNull.Value)
            {
                interests.Add(new UserInterests { name = "Dancing", isChecked = false });
            }
            else if (ds.Tables[0].Rows[0]["Dancing"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                interests.Add(new UserInterests { name = "Dancing", isChecked = false });
            }
            else //BIT value of 1 is true 
            {
                interests.Add(new UserInterests { name = "Dancing", isChecked = false });
            }

            if (ds.Tables[0].Rows[0]["Other"] == DBNull.Value || ds.Tables[0].Rows[0]["Other"].Equals(""))
            {
                interests.Add(new UserInterests { name = "Other", isChecked = false });
                userOtherInterest = "";
            }
            else
            {
                interests.Add(new UserInterests { name = "Other", isChecked = true });
                userOtherInterest = ds.Tables[0].Rows[0]["Other"].ToString();
            }

            ViewBag.UserInterests = interests;
            ViewBag.UserOtherInterest = userOtherInterest;

            //Get Dislikes
            Dating userDislikes = new Dating();
            DataSet ds2 = new DataSet();
            List<UserInterests> dislikes = new List<UserInterests>();

            ds2 = userDislikes.getDatingDislikes(accID);

            string userOtherDislike;

            //null logic for the table, if null default to 0 so the empty user interests stays empty
            if (ds2.Tables[0].Rows[0]["Movies"] == DBNull.Value)
            {
                dislikes.Add(new UserInterests { name = "DMovies", isChecked = false });
            }
            else if (ds2.Tables[0].Rows[0]["Movies"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                dislikes.Add(new UserInterests { name = "DMovies", isChecked = false });
            }
            else //BIT value of 1 is true 
            {
                dislikes.Add(new UserInterests { name = "DMovies", isChecked = true });
            }

            if (ds2.Tables[0].Rows[0]["TV"] == DBNull.Value)
            {
                dislikes.Add(new UserInterests { name = "DTV", isChecked = false });
            }
            else if (ds2.Tables[0].Rows[0]["TV"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                dislikes.Add(new UserInterests { name = "DTV", isChecked = false });
            }
            else //BIT value of 1 is true 
            {
                dislikes.Add(new UserInterests { name = "DTV", isChecked = true });
            }
            ////////////////////////////////////////////////////////////
            if (ds2.Tables[0].Rows[0]["Anime"] == DBNull.Value)
            {
                dislikes.Add(new UserInterests { name = "DAnime", isChecked = false });
            }
            else if (ds2.Tables[0].Rows[0]["Anime"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                dislikes.Add(new UserInterests { name = "DAnime", isChecked = false });
            }
            else //BIT value of 1 is true 
            {
                dislikes.Add(new UserInterests { name = "DAnime", isChecked = false });
            }

            if (ds2.Tables[0].Rows[0]["Manga"] == DBNull.Value)
            {
                dislikes.Add(new UserInterests { name = "DManga", isChecked = false });
            }
            else if (ds2.Tables[0].Rows[0]["Manga"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                dislikes.Add(new UserInterests { name = "DManga", isChecked = false });
            }
            else //BIT value of 1 is true 
            {
                dislikes.Add(new UserInterests { name = "DManga", isChecked = false });
            }

            if (ds2.Tables[0].Rows[0]["Books"] == DBNull.Value)
            {
                dislikes.Add(new UserInterests { name = "DBooks", isChecked = false });
            }
            else if (ds2.Tables[0].Rows[0]["Books"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                dislikes.Add(new UserInterests { name = "DBooks", isChecked = false });
            }
            else //BIT value of 1 is true 
            {
                dislikes.Add(new UserInterests { name = "DBooks", isChecked = false });
            }

            if (ds2.Tables[0].Rows[0]["Video Games"] == DBNull.Value)
            {
                dislikes.Add(new UserInterests { name = "DVideo Games", isChecked = false });
            }
            else if (ds2.Tables[0].Rows[0]["Video Games"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                dislikes.Add(new UserInterests { name = "DVideo Games", isChecked = false });
            }
            else //BIT value of 1 is true 
            {
                dislikes.Add(new UserInterests { name = "DVideo Games", isChecked = false });
            }

            if (ds2.Tables[0].Rows[0]["Sports"] == DBNull.Value)
            {
                dislikes.Add(new UserInterests { name = "DSports", isChecked = false });
            }
            else if (ds2.Tables[0].Rows[0]["Sports"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                dislikes.Add(new UserInterests { name = "DSports", isChecked = false });
            }
            else //BIT value of 1 is true 
            {
                dislikes.Add(new UserInterests { name = "DSports", isChecked = false });
            }

            if (ds2.Tables[0].Rows[0]["Gym"] == DBNull.Value)
            {
                dislikes.Add(new UserInterests { name = "DGym", isChecked = false });
            }
            else if (ds2.Tables[0].Rows[0]["Gym"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                dislikes.Add(new UserInterests { name = "DGym", isChecked = false });
            }
            else //BIT value of 1 is true 
            {
                dislikes.Add(new UserInterests { name = "DGym", isChecked = false });
            }

            if (ds2.Tables[0].Rows[0]["Cooking"] == DBNull.Value)
            {
                dislikes.Add(new UserInterests { name = "DCooking", isChecked = false });
            }
            else if (ds2.Tables[0].Rows[0]["Cooking"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                dislikes.Add(new UserInterests { name = "DCooking", isChecked = false });
            }
            else //BIT value of 1 is true 
            {
                dislikes.Add(new UserInterests { name = "DCooking", isChecked = false });
            }

            if (ds2.Tables[0].Rows[0]["Martial Arts"] == DBNull.Value)
            {
                dislikes.Add(new UserInterests { name = "DMartial Arts", isChecked = false });
            }
            else if (ds2.Tables[0].Rows[0]["Martial Arts"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                dislikes.Add(new UserInterests { name = "DMartial Arts", isChecked = false });
            }
            else //BIT value of 1 is true 
            {
                dislikes.Add(new UserInterests { name = "DMartial Arts", isChecked = false });
            }

            if (ds2.Tables[0].Rows[0]["Art"] == DBNull.Value)
            {
                dislikes.Add(new UserInterests { name = "DArt", isChecked = false });
            }
            else if (ds2.Tables[0].Rows[0]["Art"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                dislikes.Add(new UserInterests { name = "DArt", isChecked = false });
            }
            else //BIT value of 1 is true 
            {
                dislikes.Add(new UserInterests { name = "DArt", isChecked = false });
            }

            if (ds2.Tables[0].Rows[0]["Hiking"] == DBNull.Value)
            {
                dislikes.Add(new UserInterests { name = "DHiking", isChecked = false });
            }
            else if (ds2.Tables[0].Rows[0]["Hiking"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                dislikes.Add(new UserInterests { name = "DHiking", isChecked = false });
            }
            else //BIT value of 1 is true 
            {
                dislikes.Add(new UserInterests { name = "DHiking", isChecked = false });
            }

            if (ds2.Tables[0].Rows[0]["Partying"] == DBNull.Value)
            {
                dislikes.Add(new UserInterests { name = "DPartying", isChecked = false });
            }
            else if (ds2.Tables[0].Rows[0]["Partying"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                dislikes.Add(new UserInterests { name = "DPartying", isChecked = false });
            }
            else //BIT value of 1 is true 
            {
                dislikes.Add(new UserInterests { name = "DPartying", isChecked = false });
            }

            if (ds2.Tables[0].Rows[0]["Music"] == DBNull.Value)
            {
                dislikes.Add(new UserInterests { name = "DMusic", isChecked = false });
            }
            else if (ds2.Tables[0].Rows[0]["Music"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                dislikes.Add(new UserInterests { name = "DMusic", isChecked = false });
            }
            else //BIT value of 1 is true 
            {
                dislikes.Add(new UserInterests { name = "DMusic", isChecked = false });
            }

            if (ds2.Tables[0].Rows[0]["Dancing"] == DBNull.Value)
            {
                dislikes.Add(new UserInterests { name = "DDancing", isChecked = false });
            }
            else if (ds2.Tables[0].Rows[0]["Dancing"].Equals(false)) //the columns are BITS so they are either NULL, 0, or 1. 0 meaning false
            {
                dislikes.Add(new UserInterests { name = "DDancing", isChecked = false });
            }
            else //BIT value of 1 is true 
            {
                dislikes.Add(new UserInterests { name = "DDancing", isChecked = false });
            }

            if (ds2.Tables[0].Rows[0]["Other"] == DBNull.Value || ds2.Tables[0].Rows[0]["Other"].Equals(""))
            {
                dislikes.Add(new UserInterests { name = "DOther", isChecked = false });
                userOtherDislike = "";
            }
            else
            {
                dislikes.Add(new UserInterests { name = "DOther", isChecked = true });
                userOtherDislike = ds2.Tables[0].Rows[0]["Other"].ToString();
            }

            ViewBag.UserDislikes = dislikes;
            ViewBag.UserOtherDislike = userOtherDislike;

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
                if (state == theState)
                {
                    listOfStates.Add(new SelectListItem { Text = theState, Value = theState, Selected = true });
                }
                else
                {
                    listOfStates.Add(new SelectListItem { Value = theState, Text = theState });
                }
            }

            ViewBag.StateOptions = listOfStates;

            //create viewbag with list of image urls from photogallery table
            List<string> photos = new List<string>();

            return View("~/Views/Home/userProfile.cshtml", editProfile);
        }

        public ActionResult LikeProfile(int accID)
        {
            //create a like record between the user and the viewed profile
            Dating like = new Dating();
            int initiator = Convert.ToInt32(HttpContext.Session.GetString("accountID"));
            int recipient = accID;
            bool status = false;

            like.createLikeRequest(initiator, recipient, status);
            //Debug.WriteLine("Profile " + initiator + " and Profile " + recipient + " have a like request");
            return RedirectToAction("Likes", "Likes");
        }
    }
}
