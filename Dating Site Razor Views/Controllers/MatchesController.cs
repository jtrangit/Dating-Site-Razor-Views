using Dating_Site_Razor_Views.Views.Likes;
using Dating_Site_Razor_Views.Views.Matches;
using DatingSiteLibrary;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;

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

            //received dates
            DataTable dataTable1 = new DataTable();
            dataTable1.Columns.Add(new DataColumn("Name", typeof(string)));
            dataTable1.Columns.Add(new DataColumn("ProfileImg", typeof(string)));
            dataTable1.Columns.Add(new DataColumn("otherUserID", typeof(int)));

            Dating receivedDate = new Dating();
            DataSet receivedOtherData = new DataSet();

            receivedOtherData = receivedDate.getReceivedDates(userID);

            int rDatePartnerID;

            for(int i = 0; i < receivedOtherData.Tables[0].Rows.Count; i++)
            {
                rDatePartnerID = Convert.ToInt32(receivedOtherData.Tables[0].Rows[i]["User1"]);

                Dating dating = new Dating();
                DataSet dataSet = new DataSet();

                dataSet = dating.getProfileInfo(rDatePartnerID);

                string rOtherName = (string)dataSet.Tables[0].Rows[0]["Name"];
                string rOtherProfilePic = (string)dataSet.Tables[0].Rows[0]["ProfileImg"];
                int rOtherID = (int)dataSet.Tables[0].Rows[0]["AccountID"];

                DataRow row = dataTable1.NewRow();
                row["Name"] = rOtherName;
                row["ProfileImg"] = rOtherProfilePic;
                row["otherUserID"] = rOtherID;

                dataTable1.Rows.Add(row);
            }

            DataSet bindReceived = new DataSet();
            bindReceived.Tables.Add(dataTable1);

            List<MatchedProfile> receivedDates = new List<MatchedProfile>();

            foreach(DataRow row in bindReceived.Tables[0].Rows)
            {
                MatchedProfile received = new MatchedProfile();
                received.Name = (string)row["Name"];
                received.ProfileImg = (string)row["ProfileImg"];
                received.otherUserID = (int)row["otherUserID"];
                receivedDates.Add(received);
            }

            ViewBag.ReceivedDates = receivedDates;

            return View("~/Views/Matches/Matches.cshtml");
        }

        public ActionResult RequestDate(int accID)
        {
            int userID = Convert.ToInt32(HttpContext.Session.GetString("accountID")); //current user 
            int otherUser = accID;

            Dating dateRequest = new Dating();
            dateRequest.dateRequest(userID, otherUser);

            Dating deleteMatch = new Dating();
            deleteMatch.unMatchOnDate(userID, otherUser);

            return RedirectToAction("Matches", "Matches");
        }

        public ActionResult UnMatch(int accID)
        {
            int userID = Convert.ToInt32(HttpContext.Session.GetString("accountID")); //current user 
            int otherUser = accID;

            Dating unmatch = new Dating();
            unmatch.unMatch(userID, otherUser);

            return RedirectToAction("Matches", "Matches");
        }

        public ActionResult AcceptDate(int accID)
        {
            int userID = Convert.ToInt32(HttpContext.Session.GetString("accountID")); //current user 
            int otherUser = accID;

            Dating updateDate = new Dating();
            updateDate.updateDates(otherUser, userID);

            Dating planDate = new Dating();
            planDate.createDatePlans(otherUser, userID);

            return RedirectToAction("Matches", "Matches");
        }

        public ActionResult DenyDate(int accID)
        {
            int userID = Convert.ToInt32(HttpContext.Session.GetString("accountID")); //current user 
            int otherUser = accID;

            Dating deleteDate = new Dating();

            deleteDate.deleteDate(userID, otherUser);

            return RedirectToAction("Matches", "Matches");
        }
    }
}
