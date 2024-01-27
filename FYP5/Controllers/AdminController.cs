using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using FYP5.Models;
using Microsoft.IdentityModel.Tokens;

namespace FYP5.Controllers;

public class AdminController : Controller
{


    public IActionResult Bar()
    {
        PrepareData();
        ViewData["Chart"] = "bar";
        ViewData["Title"] = "No. of Review Rating";
        ViewData["ShowLegend"] = "false";
        return View("Chart");
    }


    private void PrepareData()
    {
        int[] dataRating = new int[5];
        List<Reviews> list = DBUtl.GetList<Reviews>("SELECT * FROM Reviews");

        foreach (Reviews review in list)
        {
            int ratingIndex = CalcRating(review.Rating ?? 0);
            dataRating[ratingIndex]++;
        }
        ViewData["Data"] = dataRating;
    }
        private int CalcRating(int rate)
        {
        if (rate == 1) return 1;
        else if (rate == 2) return 2;
        else if (rate == 3) return 3;
        else if (rate == 4) return 4;
        else if (rate == 5) return 5;
        else return 0;
        }
    }





















/*public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Total number of users who upload pictures / no. of registered users
        var totalUsers = await _context.JiakUser.CountAsync();
        var usersWithUploads = await _context.ImageUploads.Select(iu => iu.UserID).Distinct().CountAsync();

        // Number of users
        ViewBag.TotalUsers = totalUsers;
        ViewBag.UsersWithUploads = usersWithUploads;

        // View all the uploaded dishes for every user
        var uploadedDishes = await _context.ImageUploads
            .Include(iu => iu.User)
            .Include(iu => iu.Dishes)
            .ToListAsync();

        var adminSummaryViewModel = new AdminSummaryViewModel
        {
            TotalUsers = totalUsers,
            UsersWithUploads = usersWithUploads,
            UploadedDishes = uploadedDishes
        };

        return View(adminSummaryViewModel);
    }

    public IActionResult MostLeastOrderedDish()
    {
        // Most / Least ordered dish
        var mostOrderedDish = _context.Food
            .GroupBy(f => f.FoodOne)
            .OrderByDescending(g => g.Count())
            .Select(g => new DishViewModel { DishName = g.Key, OrderCount = g.Count() })
            .FirstOrDefault();

        var leastOrderedDish = _context.Food
            .GroupBy(f => f.FoodOne)
            .OrderBy(g => g.Count())
            .Select(g => new DishViewModel { DishName = g.Key, OrderCount = g.Count() })
            .FirstOrDefault();

        var mostLeastOrderedDishViewModel = new MostLeastOrderedDishViewModel
        {
            MostOrderedDish = mostOrderedDish,
            LeastOrderedDish = leastOrderedDish
        };

        return View(mostLeastOrderedDishViewModel);
    }

    public IActionResult ReviewRatingChart()
    {
        // Review rating (bar chart of 1-5 rating)
        var ratingCounts = _context.Reviews
            .GroupBy(r => r.Rating)
            .OrderBy(r => r.Key)
            .Select(g => new ReviewRatingCount { Rating = g.Key, Count = g.Count() })
            .ToList();

        var reviewRatingChartViewModel = new ReviewRatingChartViewModel
        {
            RatingCounts = ratingCounts
        };

        return View(reviewRatingChartViewModel);
    }
}*/
