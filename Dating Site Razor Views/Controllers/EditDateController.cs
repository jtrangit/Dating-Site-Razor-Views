using Dating_Site_Razor_Views.Views.EditDate;
using DatingSiteLibrary;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;

namespace Dating_Site_Razor_Views.Controllers
{
    public class EditDateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EditDate(int partnerID)
        {
            int user1 = Convert.ToInt32(HttpContext.Session.GetString("accountID")); //current user 
            int user2 = partnerID;

            Dating dateDetails = new Dating();
            DataSet ds = new DataSet();
            ds = dateDetails.getDateDetails(user1, user2);

            string date;
            string time;
            string description;
            string name;

            if (ds.Tables[0].Rows[0]["Date"].Equals(DBNull.Value) || ds.Tables[0].Rows[0]["Date"].Equals(null))
            {
                date = string.Empty;
            }
            else
            {
                date = Convert.ToDateTime(ds.Tables[0].Rows[0]["Date"]).ToString("yyyy-MM-dd");
            }
            if (ds.Tables[0].Rows[0]["Time"].Equals(DBNull.Value) || ds.Tables[0].Rows[0]["Time"].Equals(null))
            {
                time = string.Empty;
            }
            else
            {
                time = Convert.ToDateTime(ds.Tables[0].Rows[0]["Time"]).ToString("HH/mm");
            }
            if (ds.Tables[0].Rows[0]["Description"].Equals(DBNull.Value))
            {
                description = "NOT SET";
            }
            else
            {
                description = ds.Tables[0].Rows[0]["Description"].ToString();
            }

            Dating getPartnerName = new Dating();
            DataSet partnerName = new DataSet();

            partnerName = getPartnerName.getProfileInfo(user2);

            name = partnerName.Tables[0].Rows[0]["Name"].ToString();

            Date theDate = new Date();
            theDate.partnerName = name;
            theDate.date = date;
            theDate.description = description;
            theDate.time = time;
            theDate.partnerID = user2;

            ViewBag.TheDate = theDate;

            return View("~/Views/EditDate/EditDate.cshtml");
        }

        [HttpPost]
        public ActionResult UpdateDetails(int accID)
        {
            int user1 = Convert.ToInt32(HttpContext.Session.GetString("accountID")); //current user 
            int user2 = accID;

            string date;
            string time;
            string description;

            date = Request.Form["theDate"];
            time = Request.Form["theTime"];
            description = Request.Form["txtDescription"];

            Dating update = new Dating();
            update.updateDateDetails(user1, user2, date, time, description);

            Debug.WriteLine("user2 ID: " + accID);
            Debug.WriteLine("date: " + date);
            Debug.WriteLine("time: " + time);
            Debug.WriteLine("description: " + description);
            return RedirectToAction("Dates", "Dates");
        }
    }
}
