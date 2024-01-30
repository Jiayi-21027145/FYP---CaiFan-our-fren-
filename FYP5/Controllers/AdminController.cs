using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using FYP5.Models;
using static FYP5.Models.AdminSummary;

namespace FYP5.Controllers
{
    public class AdminSummaryController : Controller
    {
        private readonly AppDbContext _context;

        public AdminSummaryController(AppDbContext context)
        {
            _context = context;
        

       /* public ActionResult ImageUploadsChart()
        {
            // Assume _context is your database context
            var imageCountByUser = _context.History
                .GroupBy(h => h.UserId)
                .Select(group => new
                {
                    UserId = group.Key,
                    UploadCount = group.Count()
                })
                .ToList();

            // Convert the anonymous type to a view model if necessary, or pass it directly to the view
            return View(imageCountByUser);
        }*/
    }


public IActionResult AdminIndex()
{
    AdminSummary adminSummary = new AdminSummary();

    // 1. Total upload of images by all users based on year
    adminSummary.TotalImageUploads = _context.History
        .Count(h => h.UploadDate.Year == DateTime.Now.Year);

    // 2. Table with a dropdown list of all the locations, showing top 10 dish count for the selected location
    List<LocationDishCount> topDishesByLocation = _context.History
        .GroupBy(h => h.Location)
        .Select(g => new LocationDishCount
        {
            LocationName = g.Key,
            DishCount = g.Sum(h => h.DishOne != null ? 1 : 0)
                         + g.Sum(h => h.DishTwo != null ? 1 : 0)
                         + g.Sum(h => h.DishThree != null ? 1 : 0)
                         + g.Sum(h => h.DishFour != null ? 1 : 0)
                         + g.Sum(h => h.DishFive != null ? 1 : 0)
                         + g.Sum(h => h.DishSix != null ? 1 : 0)
        })
        .OrderByDescending(ldc => ldc.DishCount)
        .Take(10)
        .ToList();

    adminSummary.TopDishesByLocation = topDishesByLocation;

    // 3. Review rating (bar chart of 1-5 rating)
    List<ReviewRatingCount> reviewRatingCounts = _context.Reviews
        .GroupBy(r => r.Rating)
        .Select(g => new ReviewRatingCount
        {
            Rating = g.Key,
            Count = g.Count()
        })
        .OrderBy(rrc => rrc.Rating)
        .ToList();

    adminSummary.ReviewRatingCounts = reviewRatingCounts;

    return View(adminSummary);
}
    }
}


/*using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace FYP5.Controllers;
public class AdminController : Controller
{
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
        _context = context;
    }
    public IActionResult Bar()
    {
        PrepareData(0);
        ViewData["Chart"] = "bar";
        ViewData["Title"] = "Rating Summary";
        ViewData["ShowLegend"] = "false";
        return View("Chart");
    }
    private void PrepareData(int x)
    {
        int[] dataRating = new int[] { 0, 0, 0, 0, 0 };

        List<Reviews> list = DBUtl.GetList<Reviews>("SELECT * FROM Reviews");

        foreach (Reviews r in list)
        {
            dataRating[CalcGrade(r.Rating)]++;
        }
        
        string[] colors = new[] { "cyan", "lightgreen", "yellow", "pink", "lightgrey" };
        string[] rate = new[] { "1", "2", "3", "4", "5" };
        ViewData["Legend"] = "Rating";
        ViewData["Colors"] = colors;
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

    *//*using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FYP5.Models;

    namespace FYP5.Controllers
    {
        public class AdminController : Controller
        {
            private readonly AppDbContext _context;

            public AdminController(AppDbContext context)
            {
                _context = context;
            }*//*

    public IActionResult Chart()
        {

            var userActivityData = PrepareUserActivityData();
            var totalPicsPerUserData = PrepareTotalPicsPerUserData();
            var orderedDishData = PrepareOrderedDishData();
            var locationData = PrepareLocationData();
            var reviewRatingData = PrepareReviewRatingData();

            // Pass all data to the view
            ViewData["UserActivityData"] = userActivityData;
            ViewData["TotalPicsPerUserData"] = totalPicsPerUserData;
            ViewData["OrderedDishData"] = orderedDishData;
            ViewData["LocationData"] = locationData;
            ViewData["ReviewRatingData"] = reviewRatingData;

            return View();
        }


        public IActionResult UserActivity()
        {
            var data = PrepareUserActivityData();
            PrepareChartData("bar", "Total Users by Year and Month", "true", data);

            return View("Chart");
        }

        public IActionResult TotalPicsPerUser()
        {
            var data = PrepareTotalPicsPerUserData();
            PrepareChartData("table", "Total Images Uploaded per User", "true", data);

            return View("Chart");
        }

        public IActionResult OrderedDish()
        {
            var data = PrepareOrderedDishData();
            PrepareChartData("bar", "Count of Dish Ordered", "true", data);

            return View("Chart");
        }

        public IActionResult LocationData()
        {
            *//*            var locations = GetLocations(); // Fetch the list of locations
                        var data = PrepareLocationData(); // Your existing data preparation logic

                        PrepareChartData("bar", "Rank Dishes by Location", "true", data);

                        ViewBag.Locations = new SelectList(locations, "Id", "Name"); // Pass locations to the view
                        ViewBag.ChartData = data;

                        return View("LocationData");*//*

            var data = PrepareLocationData();
            PrepareChartData("bar", "Rank Dishes by Location", "true", data);

            return View("Chart");
        }
        private List<Location> GetLocations()
        {
            // Replace this with your actual method to fetch locations
            // Example:
            return new List<Location>
        {
            new Location { LocationName = "Location 1" },
           *//* new Location { LocationId = 2, Name = "Location 2" },*//*
            // Add more locations as needed
        };
        }

        public IActionResult ReviewRating()
        {
            var data = PrepareReviewRatingData();
            PrepareChartData("bar", "Review Rating Ranking", "true", data);

            return View("Chart");
        }

        private void PrepareChartData(string chartType, string title, string showLegend, object data)
        {
            ViewData["ChartType"] = chartType;
            ViewData["Title"] = title;
            ViewData["ShowLegend"] = showLegend;
            ViewData["ChartData"] = data;
        }

        // Implement the data preparation methods below

        private object PrepareUserActivityData()
        {
            // Example SQL query (replace it with your actual query)
            string sql = "SELECT YEAR(LastLogin) AS Year, MONTH(LastLogin) AS Month, COUNT(UserId) AS TotalUsers FROM JiakUser WHERE LastLogin IS NOT NULL GROUP BY YEAR(LastLogin), MONTH(LastLogin)";

            var data = DBUtl.GetList(sql);
            return data;
        }

        private object PrepareTotalPicsPerUserData()
        {

            string sql = "SELECT UserId as UserName, COUNT(Id) AS TotalUploads " +
                 "FROM History " +
                 "GROUP BY UserId";

            var data = DBUtl.GetList(sql);
            return data;
        }

        private object PrepareOrderedDishData()
        {
            // Example SQL query to get the most and least ordered dishes (excluding empty dishes)
            string sql = "SELECT TOP 2 Dish, COUNT(*) AS OrderCount " +
                        "FROM (" +
                        "    SELECT DishOne AS Dish FROM History WHERE DishOne IS NOT NULL AND DishOne <> '' " +
                        "    UNION ALL " +
                        "    SELECT DishTwo FROM History WHERE DishTwo IS NOT NULL AND DishTwo <> '' " +
                        "    UNION ALL " +
                        "    SELECT DishThree FROM History WHERE DishThree IS NOT NULL AND DishThree <> '' " +
                        "    UNION ALL " +
                        "    SELECT DishFour FROM History WHERE DishFour IS NOT NULL AND DishFour <> '' " +
                        "    UNION ALL " +
                        "    SELECT DishFive FROM History WHERE DishFive IS NOT NULL AND DishFive <> '' " +
                        "    UNION ALL " +
                        "    SELECT DishSix FROM History WHERE DishSix IS NOT NULL AND DishSix <> '' " +
                        ") AS AllDishes " +
                        "GROUP BY Dish " +
                        "ORDER BY OrderCount DESC";

            var orderedDishes = DBUtl.GetList(sql);

            // Create a list to hold the ordered dish data with colors
            var data = orderedDishes.Select(dish => new
            {
                DishName = dish.Dish,
                OrderCount = dish.OrderCount,
                Color = dish.OrderCount == orderedDishes.Max(d => d.OrderCount) ? "green" : "orange"
            }).ToList<object>();

            return data;
        }


        private object PrepareLocationData()
        {
            // Example SQL query to get the top 10 dishes for each location
            string sql = "SELECT Location, Dish, COUNT(*) AS OrderCount " +
                        "FROM (" +
                        "    SELECT Location, DishOne AS Dish FROM History WHERE DishOne IS NOT NULL AND DishOne <> '' " +
                        "    UNION ALL " +
                        "    SELECT Location, DishTwo FROM History WHERE DishTwo IS NOT NULL AND DishTwo <> '' " +
                        "    UNION ALL " +
                        "    SELECT Location, DishThree FROM History WHERE DishThree IS NOT NULL AND DishThree <> '' " +
                        "    UNION ALL " +
                        "    SELECT Location, DishFour FROM History WHERE DishFour IS NOT NULL AND DishFour <> '' " +
                        "    UNION ALL " +
                        "    SELECT Location, DishFive FROM History WHERE DishFive IS NOT NULL AND DishFive <> '' " +
                        "    UNION ALL " +
                        "    SELECT Location, DishSix FROM History WHERE DishSix IS NOT NULL AND DishSix <> '' " +
                        ") AS AllDishes " +
                        "GROUP BY Location, Dish " +
                        "ORDER BY Location, OrderCount DESC";

            var locationData = DBUtl.GetList(sql);

            // Group the location data by location
            var groupedByLocation = locationData.GroupBy(ld => ld.Location);

            // Create a list to hold the location data with top 10 dishes for each location
            var data = new List<object>();
            foreach (var locationGroup in groupedByLocation)
            {
                var location = locationGroup.Key;
                var top10Dishes = locationGroup.Take(10).Select(dish => new
                {
                    DishName = dish.Dish,
                    OrderCount = dish.OrderCount
                }).ToList<object>();

                data.Add(new
                {
                    Location = location,
                    Top10Dishes = top10Dishes
                });
            }

            return data;
        }

        private object PrepareLocationData2()
        {
            // Implement logic to fetch data for LocationData chart
            // ...

            // Sample data for testing
            var data = new List<object> {
                new { Location = "Location1", Rank = 1 },
                new { Location = "Location2", Rank = 2 },
                // Add more data...
            };

            return data;
        }

        private object PrepareReviewRatingData()
        {
            // Example SQL query to get the count of reviews for each rating
            string sql = "SELECT Rating, COUNT(*) AS Count FROM Reviews GROUP BY Rating ORDER BY Rating";

            var reviewRatingData = DBUtl.GetList(sql);

            // Create a list to hold the review rating data
            var data = new List<object>();
            for (int rating = 1; rating <= 5; rating++)
            {
                var ratingCount = reviewRatingData.FirstOrDefault(rr => rr.Rating == rating)?.Count ?? 0;
                data.Add(new
                {
                    Rating = rating,
                    Count = ratingCount
                });
            }

            return data;
        }




    }



*/