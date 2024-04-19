using Dating_Site_Razor_Views.Pages;
using DatingSiteLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

            return View("~/Views/Home/userProfile.cshtml", editProfile);
        }
    }
}
