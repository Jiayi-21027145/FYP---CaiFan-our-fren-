using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using FYP5.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FYP5.Controllers;

public class AdminController : Controller
{
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
        _context = context;
    }

/*    public IActionResult UserActivity() //bar, filter by year month for total no. of users
    {
        PrepareDataOne(1);
        ViewData["Chart"] = "bar";
        ViewData["Title"] = "Total Users by Year and Month";
        ViewData["ShowLegend"] = "true";

        return View("Chart");
    }

    public ActionResult TotalPicsPerUser()
    {
        PrepareDataTwo(2);
        ViewData["Chart"] = "table"; //need to do the <table> <body> in prepdata2
        ViewData["Title"] = "Total Images Uploaded per User";
        ViewData["ShowLegend"] = "true";

        return View("Chart");
    }

    public ActionResult OrderedDish() //most and least ordered
    {
        PrepareDataThree(3);
        ViewData["Chart"] = "bar";
        ViewData["Title"] = "Count of Dish Ordered";
        ViewData["ShowLegend"] = "true";

        return View("Chart");
    }

    public ActionResult LocationData()  //interactive 
    {
        PrepareDataFour(4);
        ViewData["Chart"] = "bar";
        ViewData["Title"] = "Rank Dishes by Location";
        ViewData["ShowLegend"] = "true";

        return View("Chart");
    }*/


    public ActionResult ReviewRating()
    {
        PrepareDataFive(5);
        ViewData["Chart"] = "bar";
        ViewData["Title"] = "Review Rating Ranking";
        ViewData["ShowLegend"] = "true";

        return View("Chart");
    }


/*    private void PrepareDataOne(int a)
    {

    }

    private void PrepareDataTwo(int b) //total upload for each user (table)
    {
        List<History> list = DBUtl.GetList<History>("SELECT * FROM History");
        foreach (History history in list)
        {
            var groupedData = historyList
        .GroupBy(h => h.UserId)
        .Select(group => new UserPictureUploadViewModel
        {
            UserId = group.Key,
            UserName = GetUserDisplayName(group.Key), // You need to implement a method to get user display names
            TotalUploads = group.Count()
        })
        .ToList();

            return groupedData;
        }
    }

    private void PrepareDataThree(int c)
    {
        List<History> list = DBUtl.GetList<History>("SELECT * FROM History");
        List<string> dishNames = new List<string>();
        List<int> dishCounts = new List<int>();

        // Loop through DishOne to DishSix
        for (int i = 1; i <= 6; i++)
        {
            string dishProperty = $"Dish{i}";

            foreach (History history in list)
            {
                // Check if the dish property is not null
                if (!string.IsNullOrEmpty(history.GetType().GetProperty(dishProperty)?.GetValue(history)?.ToString()))
                {
                    // Get the dish name
                    string dishName = history.GetType().GetProperty(dishProperty)?.GetValue(history)?.ToString();

                    // Check if the dish is already in the list
                    int index = dishNames.IndexOf(dishName);
                    if (index >= 0)
                    {
                        // Increment dish count
                        dishCounts[index]++;
                    }
                    else
                    {
                        // Add the new dish and set count to 1
                        dishNames.Add(dishName);
                        dishCounts.Add(1);
                    }
                }
            }
        }

        // Pass data to the view
        ViewData["Data"] = dishCounts;
        ViewData["Labels"] = dishNames;
    }

    private void PrepareDataFour(int x)
    {
        int dataRank = new int();
        List<History> list = DBUtl.GetList<History>("SELECT * FROM History");

        foreach (History history in list)
        {
            int rankIndex = CalcRanking(history.Ranking ?? 0);
            dataRank[ratingIndex]++;
        }
        ViewData["Data"] = dataRank;
    }*/
    

    private void PrepareDataFive(int y)
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





/*    public async Task<IActionResult> Index()
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
}
*/











/*public class AdminController : Controller
{


    public IActionResult ReviewRatingBar()
    {

        string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        string query = "SELECT Rating, COUNT(*) AS Count FROM Reviews GROUP BY Rating ORDER BY Rating";


        var reviewRatings = _context.Reviews
            .GroupBy(r => r.Rating)
            .Select(g => new { Rating = g.Key, Count = g.Count() })
            .OrderBy(r => r.Rating)
            .ToList();

        PrepareData(reviewRatings, "Review Ratings", "Review Rating Distribution", "Rating", "Count", "Blue");

        return View("Chart");
    }

    private void PrepareData(IEnumerable<object>data, string charType, string title, string labels, string dataField, string color)
    {
        ViewData["Chart"] = charType;
        ViewData["Title"] = title;
        ViewData["ShowLegend"] = "true";
        ViewData["Legend"] = "Data";
        ViewData["Colors"] = new[] { color };
        ViewData["Labels"] = labels;
        ViewData["Data"] = dataField;
        ViewData["ChartData"] = data;
    }
*/

        /*PrepareData();
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
        }*/
    






















