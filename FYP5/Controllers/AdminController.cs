using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;
using FYP5.Models;

namespace FYP5.Controllers;
public class AdminController : Controller
{
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
        _context = context;
    }

    //THIS IS BAR CHART FOR REVIEW RATING SUMMARY
    public IActionResult Bar()
    {
        PrepareData(0);
        ViewData["Chart"] = "bar";
        ViewData["Title"] = "Rating Summary";
        ViewData["ShowLegend"] = "false";
        return View("ReviewRatingChart");
    }
    private void PrepareData(int x)
    {
        int[] dataRating = new int[] { 0, 0, 0, 0, 0 };

        List<Reviews> list = DBUtl.GetList<Reviews>("SELECT * FROM Reviews");

        foreach (Reviews r in list)
        {
            dataRating[CalcGrade(r.Rating)]++;
        }

        string[] rate = new[] { "1", "2", "3", "4", "5" };
        ViewData["Legend"] = "Rating";
        ViewData["Labels"] = rate;
        if (x == 0)
            ViewData["Data"] = dataRating;
    }

    private int CalcGrade(int num)
    {
        if (num == 1) return 0;
        else if (num == 2) return 1;
        else if (num == 3) return 2;
        else if (num == 4) return 3;
        else return 4;
    }


    //THIS IS THE LINE CHART FOR THE NO. OF UPLOADS PER YEAR  
    public IActionResult Line()
    {

        return View("UploadsPerYearChart");
    }
    public IActionResult UploadsPerYearChart()
    {
        var imageCount = _context.History
            .GroupBy(h => h.UploadDate.Year)
            .Select(group => new { Year = group.Key, Count = group.Count() })
            .OrderBy(x => x.Year)
            .ToList();

        return Json(imageCount);
    }



    //THIS IS THE TABLE CHART FOR THE LOCATION'S TOP10 DISHES 
    public IActionResult Table()
    {
        // This action should populate the dropdown
        ViewBag.Locations = _context.History
            .Select(h => h.Location)
            .Distinct()
            .OrderBy(l => l) // It's a good practice to order the locations
            .ToList();

        return View("LocationTopDishes");
    }

    [HttpGet]  //action should only respond to get requests 
    public IActionResult GetTopDishes(string location)
    {
        // This action fetches the top dishes for a given location
        var topDishes = _context.History
            .Where(h => h.Location == location)
            .Select(h => new List<string> { h.DishOne, h.DishTwo, h.DishThree, h.DishFour, h.DishFive, h.DishSix })
            .AsEnumerable()  // Switch to LINQ to Objects
            .SelectMany(dishes => dishes)
            .Where(d => !string.IsNullOrEmpty(d)) // Exclude empty or null dishes
            .GroupBy(d => d)
            .Select(group => new { Dish = group.Key, Count = group.Count() })
            .OrderByDescending(g => g.Count)
            .Take(10)
            .ToList();

        return Json(topDishes);
    }


}

