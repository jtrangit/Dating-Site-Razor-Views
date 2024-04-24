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

            return View("~/Views/Matches/Matches.cshtml");
        }
    }
}
