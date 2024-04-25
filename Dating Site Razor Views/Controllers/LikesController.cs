using Dating_Site_Razor_Views.Pages;
using Dating_Site_Razor_Views.Views.Likes;
using DatingSiteLibrary;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System.Numerics;

namespace Dating_Site_Razor_Views.Controllers
{
    public class LikesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Likes()
        {
            if (!HttpContext.Session.TryGetValue("accountID", out _))
            {
                return RedirectToAction("Login", "Home");
            }
            //Get sent likes
            int user = Convert.ToInt32(HttpContext.Session.GetString("accountID")); //current user 
            int otherPerson; //person who the user will have liked

            Dating getOtherUserID = new Dating();
            DataSet ds = new DataSet();
            ds = getOtherUserID.getSentLikes(user);

            DataTable populateTable = new DataTable();
            populateTable.Columns.Add(new DataColumn("Name", typeof(string)));
            populateTable.Columns.Add(new DataColumn("ProfileImg", typeof(string)));
            populateTable.Columns.Add(new DataColumn("Status", typeof(string)));

            //for every like record
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //get the recipient's account ID
                otherPerson = Convert.ToInt32(ds.Tables[0].Rows[i]["Recipient"]);
                string status;

                if (ds.Tables[0].Rows[i]["Status"].Equals(false))
                {
                    status = "Pending Approval";
                }
                else
                {
                    status = "Approved";
                }

                //get the recipient's profile info
                Dating otherUser = new Dating();
                DataSet otherUserInfo = new DataSet();
                otherUserInfo = otherUser.getProfileInfo(otherPerson);

                string recipientName = (string)otherUserInfo.Tables[0].Rows[0]["Name"];
                string recipientProfilePic = (string)otherUserInfo.Tables[0].Rows[0]["ProfileImg"];

                DataRow row = populateTable.NewRow();
                row["Name"] = recipientName;
                row["ProfileImg"] = recipientProfilePic;
                row["Status"] = status;

                populateTable.Rows.Add(row);
            }

            DataSet bindingSet = new DataSet();
            bindingSet.Tables.Add(populateTable);

            List<likedProfile> likes = new List<likedProfile>();

            foreach(DataRow row in bindingSet.Tables[0].Rows)
            {
                likedProfile likedProfile = new likedProfile();
                likedProfile.name = row["Name"].ToString();
                likedProfile.profilePic = row["ProfileImg"].ToString();
                likedProfile.status = row["Status"].ToString();
                likes.Add(likedProfile);
            }
            
            ViewBag.SentLikes = likes;

            //Get received likes
            Dating getSender = new Dating();
            DataSet ds2 = new DataSet();
            ds2 = getSender.getReceivedLikes(user);

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("ProfileImg", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Status", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Initiator", typeof(int)));

            //for every like record
            for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
            {
                otherPerson = Convert.ToInt32(ds2.Tables[0].Rows[i]["Initiator"]);

                string status;

                if (ds2.Tables[0].Rows[i]["Status"].Equals(false))
                {
                    status = "Pending Your Approval";
                }
                else
                {
                    status = "Approved";
                }

                //get the initiator's profile info
                Dating otherUser = new Dating();
                DataSet otherUserInfo = new DataSet();
                otherUserInfo = otherUser.getProfileInfo(otherPerson);

                string initiatorName = (string)otherUserInfo.Tables[0].Rows[0]["Name"];
                string initiatorProfilePic = (string)otherUserInfo.Tables[0].Rows[0]["ProfileImg"];

                DataRow row = dataTable.NewRow();
                row["Name"] = initiatorName;
                row["ProfileImg"] = initiatorProfilePic;
                row["Status"] = status;
                row["Initiator"] = otherPerson;

                dataTable.Rows.Add(row);
            }

            DataSet ds3 = new DataSet();
            ds3.Tables.Add(dataTable);

            List<likedProfile> receivedLikes = new List<likedProfile>();

            foreach (DataRow row in ds3.Tables[0].Rows)
            {
                likedProfile recieved = new likedProfile();
                recieved.name = row["Name"].ToString();
                recieved.profilePic = row["ProfileImg"].ToString();
                recieved.status = row["Status"].ToString();
                recieved.initiator = row["Initiator"].ToString();
                receivedLikes.Add(recieved);
            }

            ViewBag.ReceivedLikes = receivedLikes;

            return View("~/Views/Likes/Likes.cshtml");
        }

        public IActionResult Match(string accID)
        {
            int initiator = Convert.ToInt32(accID);
            int recipient = Convert.ToInt32(HttpContext.Session.GetString("accountID")); //current user 

            Dating match = new Dating();
            Debug.WriteLine("initiator: " + initiator + ", recipient: " + recipient);
            match.createMatch(initiator, recipient);

            return RedirectToAction("Likes", "Likes");
        }

        public IActionResult DenyLike(string accID)
        {
            int initiator = Convert.ToInt32(accID);
            int recipient = Convert.ToInt32(HttpContext.Session.GetString("accountID")); //current user 

            Dating delete = new Dating();
            Debug.WriteLine("initiator: " + initiator + ", recipient: " + recipient);
            delete.deleteLikes(initiator, recipient);

            return RedirectToAction("Likes", "Likes");
        }
    }
}
