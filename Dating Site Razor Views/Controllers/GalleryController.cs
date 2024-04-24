using DatingSiteLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Data;
using System.Diagnostics;

namespace Dating_Site_Razor_Views.Controllers
{
    public class GalleryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Gallery()
        {
            if (!HttpContext.Session.TryGetValue("accountID", out _))
            {
                return RedirectToAction("Login", "Home");
            }

            //get all the photos of the user's photo gallery if any

            int accID = Convert.ToInt32(HttpContext.Session.GetString("accountID"));

            Dating dating = new Dating();
            DataSet photos = new DataSet();

            photos = dating.getPhotoGallery(accID);

            List<string> listOfPhotos = new List<string>();

            if (photos != null)
            {
                foreach (DataTable dataTable in photos.Tables)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (row["PhotoUrl"] != DBNull.Value || row["PhotoUrl"] != null)
                            listOfPhotos.Add(row["PhotoUrl"].ToString());
                    }
                }
            }
            else
            {
                listOfPhotos = null;
            }

            ViewBag.UserPhotoGallery = listOfPhotos;

            return View("Gallery", "Gallery");
        }

        [HttpPost]
        public ActionResult AddToGallery(string txtAddPhoto)
        {
            int accID = Convert.ToInt32(HttpContext.Session.GetString("accountID"));

            Dating dating = new Dating();
            dating.createPhotoGallery(accID, txtAddPhoto);

            //Debug.WriteLine("Photo Gallery Test ID: " + accID);
           // Debug.WriteLine("Photo Gallery Test Photo: " + txtAddPhoto);


            return RedirectToAction("Gallery", "Gallery");
        }

        [HttpPost]
        public ActionResult RemoveFromGallery(string removePhoto)
        {
            int accID = Convert.ToInt32(HttpContext.Session.GetString("accountID"));
            Debug.WriteLine("URL of photo being deleted: " + removePhoto);

            Dating dating = new Dating();
            dating.deletePhotoInGallery(accID, removePhoto);
            return RedirectToAction("Gallery", "Gallery");
        }

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
    }
}
