using Dating_Site_Razor_Views.Views.Dates;
using Dating_Site_Razor_Views.Views.Matches;
using DatingSiteLibrary;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Dating_Site_Razor_Views.Controllers
{
    public class DatesController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Dates()
        {
            if (!HttpContext.Session.TryGetValue("accountID", out _))
            {
                return RedirectToAction("Login", "Home");
            }

            int userID = Convert.ToInt32(HttpContext.Session.GetString("accountID")); //current user 

            //the table which we will populate the repeaters with after getting all the info from each match
            DataTable populateTable = new DataTable();
            populateTable.Columns.Add(new DataColumn("Name", typeof(string)));
            populateTable.Columns.Add(new DataColumn("ProfileImg", typeof(string)));
            populateTable.Columns.Add(new DataColumn("otherUserID", typeof(int)));
            populateTable.Columns.Add(new DataColumn("Date", typeof(string)));
            populateTable.Columns.Add(new DataColumn("Time", typeof(string)));
            populateTable.Columns.Add(new DataColumn("Description", typeof(string)));

            Dating date = new Dating();
            DataSet otherData = new DataSet();

            otherData = date.getDatePlans(userID);

            string theDate;
            string theTime;
            string theDesc;

            int datePartnerID;

            for (int i = 0; i < otherData.Tables[0].Rows.Count; i++)
            {
                if (otherData.Tables[0].Rows[i]["Person1"].Equals(userID)) //if the user is in user1, get ID for user2
                {
                    datePartnerID = Convert.ToInt32(otherData.Tables[0].Rows[i]["Person2"]);
                }
                else //if user is in user2, get ID for user1
                {
                    datePartnerID = Convert.ToInt32(otherData.Tables[0].Rows[i]["Person1"]);
                }
                if (otherData.Tables[0].Rows[i]["Date"].Equals(DBNull.Value))
                {
                    theDate = "not set";
                }
                else
                {
                    theDate = otherData.Tables[0].Rows[i]["Date"].ToString();
                }
                if (otherData.Tables[0].Rows[i]["Time"].Equals(DBNull.Value))
                {
                    theTime = "not set";
                }
                else
                {
                    theTime = otherData.Tables[0].Rows[i]["Time"].ToString();
                }
                if (otherData.Tables[0].Rows[i]["Description"].Equals(DBNull.Value))
                {
                    theDesc = "not set";
                }
                else
                {
                    theDesc = otherData.Tables[0].Rows[i]["Description"].ToString();
                }

                //get the recipient's profile info
                Dating otherUser = new Dating();
                DataSet otherUserInfo = new DataSet();
                otherUserInfo = otherUser.getProfileInfo(datePartnerID);

                string othersName = (string)otherUserInfo.Tables[0].Rows[0]["Name"];
                string othersProfilePic = (string)otherUserInfo.Tables[0].Rows[0]["ProfileImg"];
                int othersID = (int)otherUserInfo.Tables[0].Rows[0]["AccountID"];


                DataRow row = populateTable.NewRow();
                row["Name"] = othersName;
                row["ProfileImg"] = othersProfilePic;
                row["otherUserID"] = othersID;
                row["Date"] = theDate;
                row["Time"] = theTime;
                row["Description"] = theDesc;

                populateTable.Rows.Add(row);
            }

            DataSet bindingSet = new DataSet();
            bindingSet.Tables.Add(populateTable);

            List<DateDetails> dateDetails = new List<DateDetails>();

            foreach (DataRow row in bindingSet.Tables[0].Rows)
            {
                DateDetails thedetails = new DateDetails();
                thedetails.datePartner = row["Name"].ToString();
                thedetails.partnerProfilePic = row["ProfileImg"].ToString();
                thedetails.partnerID = Convert.ToInt32(row["otherUserID"]);
                thedetails.theDate = row["Date"].ToString();
                thedetails.theTime = row["Time"].ToString();
                thedetails.description = row["Description"].ToString();
                dateDetails.Add(thedetails);
            }

            ViewBag.TheDates = dateDetails;

            return View("~/Views/Dates/Dates.cshtml");
        }
    }
}
