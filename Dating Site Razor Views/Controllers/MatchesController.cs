using Dating_Site_Razor_Views.Views.Likes;
using Dating_Site_Razor_Views.Views.Matches;
using DatingSiteLibrary;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Dating_Site_Razor_Views.Controllers
{
    public class MatchesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Matches()
        {
            //matches
            int userID = Convert.ToInt32(HttpContext.Session.GetString("accountID")); //current user 

            DataTable populateTable = new DataTable();
            populateTable.Columns.Add(new DataColumn("Name", typeof(string)));
            populateTable.Columns.Add(new DataColumn("ProfileImg", typeof(string)));
            populateTable.Columns.Add(new DataColumn("otherUserID", typeof(int)));

            Dating otherUsers = new Dating();
            DataSet otherData = new DataSet();

            otherData = otherUsers.getMatches(userID);

            int matcheeID;
            for (int i = 0; i < otherData.Tables[0].Rows.Count; i++)
            {
                if (otherData.Tables[0].Rows[i]["Initiator"].Equals(userID)) //if the user was the initiator, get the recipient ID
                {
                    matcheeID = Convert.ToInt32(otherData.Tables[0].Rows[i]["Recipient"]);
                }
                else //if user was the recipient, get the initiator ID
                {
                    matcheeID = Convert.ToInt32(otherData.Tables[0].Rows[i]["Initiator"]);
                }

                //get the recipient's profile info
                Dating otherUser = new Dating();
                DataSet otherUserInfo = new DataSet();
                otherUserInfo = otherUser.getProfileInfo(matcheeID);

                string othersName = (string)otherUserInfo.Tables[0].Rows[0]["Name"];
                string othersProfilePic = (string)otherUserInfo.Tables[0].Rows[0]["ProfileImg"];
                int othersID = (int)otherUserInfo.Tables[0].Rows[0]["AccountID"];

                DataRow row = populateTable.NewRow();
                row["Name"] = othersName;
                row["ProfileImg"] = othersProfilePic;
                row["otherUserID"] = othersID;

                populateTable.Rows.Add(row);
            }

            DataSet bindingSet = new DataSet();
            bindingSet.Tables.Add(populateTable);

            List<MatchedProfile> matchee = new List<MatchedProfile>();

            foreach (DataRow row in bindingSet.Tables[0].Rows)
            {
                MatchedProfile matched = new MatchedProfile();
                matched.Name = row["Name"].ToString();
                matched.ProfileImg = row["ProfileImg"].ToString();
                matched.otherUserID = Convert.ToInt32(row["otherUserID"]);
                matchee.Add(matched);
            }

            ViewBag.Matched = matchee;

            //sent dates
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("ProfileImg", typeof(string)));
            dataTable.Columns.Add(new DataColumn("otherUserID", typeof(int)));

            Dating sentDate = new Dating();
            DataSet otherUserData = new DataSet();

            otherUserData = sentDate.getDates(userID);

            int datePartnerID;

            for (int i = 0; i < otherUserData.Tables[0].Rows.Count; i++)
            {
                if (otherUserData.Tables[0].Rows[i]["User1"].Equals(userID))
                {
                    datePartnerID = Convert.ToInt32(otherUserData.Tables[0].Rows[i]["User2"]);
                }
                else
                {
                    datePartnerID = Convert.ToInt32(otherUserData.Tables[0].Rows[i]["User1"]);
                }

                Dating dateOtherUser = new Dating();
                DataSet dateOtherInfo = new DataSet();

                dateOtherInfo = dateOtherUser.getProfileInfo(datePartnerID);

                string dateOtherName = (string)dateOtherInfo.Tables[0].Rows[0]["Name"];
                string dateOtherProfilePic = (string)dateOtherInfo.Tables[0].Rows[0]["ProfileImg"];
                
                DataRow row = dataTable.NewRow();
                row["Name"] = dateOtherName;
                row["ProfileImg"] = dateOtherProfilePic;
                row["otherUserID"] = datePartnerID;

                dataTable.Rows.Add(row);
            }

            DataSet bindSentDate = new DataSet();
            bindSentDate.Tables.Add(dataTable);

            List<MatchedProfile> sentDates = new List<MatchedProfile>();

            foreach (DataRow row in bindSentDate.Tables[0].Rows)
            {
                MatchedProfile sent = new MatchedProfile();
                sent.Name = row["Name"].ToString();
                sent.ProfileImg = row["ProfileImg"].ToString();
                sent.otherUserID = Convert.ToInt32(row["otherUserID"]);
                sentDates.Add(sent);
            }

            ViewBag.SentDates = sentDates;

            return View("~/Views/Matches/Matches.cshtml");
        }
    }
}
