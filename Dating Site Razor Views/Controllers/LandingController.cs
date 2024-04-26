using DatingSiteLibrary;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

public class LandingController : Controller
{
    private readonly Dating _dating;

    public LandingController(Dating dating)
    {
        _dating = dating;
    }

    public IActionResult Index()
    {
        // Fetch data from the database
        var totalUsers = _dating.GetTotalUsers();
        var totalDates = _dating.GetTotalDates();
        var totalMatches = _dating.GetTotalMatches();

        // Log the fetched data
        Debug.WriteLine($"Total Users (Before): {totalUsers}");
        Debug.WriteLine($"Total Dates (Before): {totalDates}");
        Debug.WriteLine($"Total Matches (Before): {totalMatches}");

        // Set data in ViewData
        ViewData["TotalUsers"] = totalUsers;
        ViewData["TotalDates"] = totalDates;
        ViewData["TotalMatches"] = totalMatches;

        return View();
    }
}