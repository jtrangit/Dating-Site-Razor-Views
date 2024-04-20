using Dating_Site_Razor_Views.Pages;
using DatingSiteLibrary;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace Dating_Site_Razor_Views.Controllers
{
    public class UserProfileController : Controller
    {
        public IActionResult UserProfile()
        {
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
            string profilePic = (string)userInfo.Tables[0].Rows[0]["ProfileImg"];
            string city = (string)userInfo.Tables[0].Rows[0]["City"];
            string state = (string)userInfo.Tables[0].Rows[0]["State"];
            string gender = (string)userInfo.Tables[0].Rows[0]["Gender"];
            string height = (string)userInfo.Tables[0].Rows[0]["Height"];
            decimal weight = (decimal)userInfo.Tables[0].Rows[0]["Weight"];
            int age = Convert.ToInt32(userInfo.Tables[0].Rows[0]["Age"]);
            string occupation = (string)userInfo.Tables[0].Rows[0]["Occupation"];
            string commitment = (string)userInfo.Tables[0].Rows[0]["Commitment"];
            string description = (string)userInfo.Tables[0].Rows[0]["Description"];

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
            //private info 
            string email = (string)userInfo.Tables[0].Rows[0]["Email"];
            string address = (string)userInfo.Tables[0].Rows[0]["Address"];
            string phoneNumber = (string)userInfo.Tables[0].Rows[0]["PhoneNumber"];

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

            List<EditProfile> theProfile = new List<EditProfile>();
            theProfile.Add(editProfile);

            editProfile.EditProf = theProfile;

            ViewBag.LoggedInUserProfile = theProfile;

            //populate the visibility dropdownbox
            List<SelectListItem> visibleOptions = new List<SelectListItem>();

            if (accVisible == true)
            {
                visibleOptions.Add(new SelectListItem { Text = "Yes", Value =  "true", Selected = true});
                visibleOptions.Add(new SelectListItem { Text = "No", Value = "false" });
            }
            else
            {
                visibleOptions.Add(new SelectListItem { Text = "Yes", Value = "true"});
                visibleOptions.Add(new SelectListItem { Text = "No", Value = "false", Selected = true});
            }
            ViewBag.VisibleOptions = visibleOptions;

            //populate dropdownbox for gender
            List<SelectListItem> genderOptions = new List<SelectListItem>();

            if(gender == "Male")
            {
                genderOptions.Add(new SelectListItem { Text = "Male", Value = "Male", Selected = true });
                genderOptions.Add(new SelectListItem { Text = "Female", Value = "Female"});
                genderOptions.Add(new SelectListItem { Text = "Non-binary", Value = "Non-binary" });
                genderOptions.Add(new SelectListItem { Text = "Other", Value = "Other" });
            }
            else if (gender == "Female")
            {
                genderOptions.Add(new SelectListItem { Text = "Male", Value = "Male"});
                genderOptions.Add(new SelectListItem { Text = "Female", Value = "Female", Selected = true });
                genderOptions.Add(new SelectListItem { Text = "Non-binary", Value = "Non-binary" });
                genderOptions.Add(new SelectListItem { Text = "Other", Value = "Other" });

            }
            else if (gender == "Non-binary")
            {
                genderOptions.Add(new SelectListItem { Text = "Male", Value = "Male"});
                genderOptions.Add(new SelectListItem { Text = "Female", Value = "Female" });
                genderOptions.Add(new SelectListItem { Text = "Non-binary", Value = "Non-binary", Selected = true });
                genderOptions.Add(new SelectListItem { Text = "Other", Value = "Other" });
            }
            else
            {
                genderOptions.Add(new SelectListItem { Text = "Male", Value = "Male"});
                genderOptions.Add(new SelectListItem { Text = "Female", Value = "Female" });
                genderOptions.Add(new SelectListItem { Text = "Non-binary", Value = "Non-binary" });
                genderOptions.Add(new SelectListItem { Text = "Other", Value = "Other" , Selected = true });
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

            return View("~/Views/Home/userProfile.cshtml", editProfile);
        }
    }
}
