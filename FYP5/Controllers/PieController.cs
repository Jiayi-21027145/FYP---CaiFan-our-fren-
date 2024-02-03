
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using FYP5.Models; // Adjust the namespace to match your project
using System.Globalization;

public class PieController : Controller
{
    private readonly AppDbContext _dbCtx; // Assuming you have a DbContext

    public PieController(AppDbContext dbCtx)
    {
        _dbCtx = dbCtx;
    }

    public IActionResult Index()
    {
        return View("Index"); // Specifies the view to return
    }

    public IActionResult DishFrequencies()
    {
        string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var histories = _dbCtx.History
            .Where(h => h.UserId == userid) // Add this line to filter by the user ID
            .ToList();
        /*string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var histories = _dbCtx.History.ToList();*/

        var dishCounts = histories
            .SelectMany(h => new[] { h.DishOne, h.DishTwo, h.DishThree, h.DishFour, h.DishFive, h.DishSix })
            .Where(dish => !string.IsNullOrWhiteSpace(dish))
            .GroupBy(dish => dish.Trim().ToLower()) // Normalize the keys
            .Select(group => new { Key = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(group.Key), Count = group.Count() }) // Convert back to Title Case for display
            .ToList();

        return Json(dishCounts);
    }


}


/*using Microsoft.AspNetCore.Mvc;
using FYP5.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace FYP5.Controllers
{
    public class PieController : Controller
    {
        public IActionResult Pie()
        {
            var chartData = PrepareData(0);
            ViewData["Chart"] = "pie";
            ViewData["Title"] = "Dish Frequency";
            ViewData["ShowLegend"] = "true";
            ViewData["ChartData"] = chartData;
            return View("Chart");
        }
        public IActionResult Line()
        {
            var chartData = PrepareData(1);
            ViewData["Chart"] = "line";
            ViewData["Title"] = "Average Price paid";
            ViewData["ShowLegend"] = "false";
            ViewData["ChartData"] = chartData;
            return View("Chart");
        }
        private List<int> PrepareData(int x)
        {
            string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            List<History> list = DBUtl.GetList<History>("SELECT * FROM History");


            // Dictionary to keep track of dish counts
            var dishCounts = new Dictionary<string, int>();

            foreach (History historyItem in list)
            {
                CountDish(dishCounts, historyItem.DishOne);
                CountDish(dishCounts, historyItem.DishTwo);
                CountDish(dishCounts, historyItem.DishThree);
                CountDish(dishCounts, historyItem.DishFour);
                CountDish(dishCounts, historyItem.DishFive);
                CountDish(dishCounts, historyItem.DishSix);
            }

            // Convert dishCounts to the format required for chart data
            var dataDish = dishCounts.Values.ToList();
            var dishNames = dishCounts.Keys.ToList();

            ViewData["Legend"] = dishNames;
            ViewData["Colors"] = new[] { "cyan", "lightgreen", "yellow", "pink", "lightgrey", "lightpurple", "red", "orange", "lightblue", "brown", "gold" };

            return dataDish;
        }

        private void CountDish(Dictionary<string, int> dishCounts, string dish)
        {
            if (!string.IsNullOrEmpty(dish))
            {
                if (dishCounts.ContainsKey(dish))
                {
                    dishCounts[dish]++;
                }
                else
                {
                    dishCounts.Add(dish, 1);
                }
            }
        }

        private void PrepareData(int x)
        {

            int[] dataMax = new[] { 0, 0, 0, 0, 0 };
            List<History> list = DBUtl.GetList<History>("SELECT * FROM History ORDERED BY UploadDate");
            foreach (History cdt in list)
            {

                dataMax[CalcCal(cdt.AverageCalories)]++;
            }


            string[] user = new[] { "Calorie Count < 700", "Calorie Count > 700" };

            ViewData["Legend"] = "Healthy";
            // ViewData["Colors"] = colors;
            //ViewData["Labels"] = user;
            if (x == 2)
                ViewData["Data"] = dataMax;
        }

        private static int CalcCal(int c)
        {
            if (c > 700) return 1;

            else return 0;
        }
    }
}
}
*/

/* private readonly AppDbContext _context;

 public ChartController(AppDbContext context)
 {
     _context = context;
 }

 public IActionResult Chart(int? year)
 {
     DbSet<History> dbs = _context.History;
     List<History> model = null!;
     var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
     model = dbs.Where(mo => mo.UserId == userId).ToList();
     // Fetch distinct years to populate the year filter dropdown
     var years = _context.History
         .Where(h => h.UserId == userId)
         .Select(h => h.UploadDate.Year)
         .Distinct()
         .OrderBy(y => y)
         .ToList();


     // Ensure there are years to choose from
     if (!years.Any())
     {
         // Handle the case where there are no years
         // Set an error message or return a different view
     }

     // Fetch the data for the chart
     var data = model;
     if (year.HasValue)
     {
         data = data.Where(h => h.UploadDate.Year == year.Value).ToList();
     }


    if (!data.Any())
{
 // Handle the case where there's no data
 // Set an error message or return a different view
 ViewData["Message"] = $"No data available for the year {year}.";
 return View();
}

var groupedData = data
         .GroupBy(h => h.UploadDate.Month)
         .Select(group => new
         {
             Month = group.Key,
             AverageCalories = (int)group.Sum(h => h.AverageCalories)
         })
         .OrderBy(x => x.Month)
         .ToList();
     var yr = years.ToList().Select(y => new { Value = y, Text = y.ToString() }).ToList();
     var yearsSelectList = new SelectList(years, "Value", "Text", year);
     var viewModel = new Chart
     {

         *//*Years = new SelectList(years),*//*
         SelectedYear = year,
         Years = new SelectList(years.Select(y => new { Value = y, Text = y.ToString() }), "Value", "Text"),
         UserId = userId,
         Months = groupedData.Select(c => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(c.Month)).ToList(),
         AverageCalories = groupedData.Select(c => c.AverageCalories).ToList()

     };

     return View(viewModel);
 }
}
}




*//* public IActionResult Chart(int? year)
 {
     // Ensure that the context is not null.
     if (_context == null)
     {
         throw new InvalidOperationException("Database context is not initialized.");
     }

     // Ensure that the History set is not null.
     if (_context.History == null)
     {
         throw new InvalidOperationException("The History set is not available in the database context.");
     }

     var data = _context.History.ToList(); // Safely assuming _context and History are properly initialized and not null.

     // Check if there's any data fetched from the database.
     if (!data.Any())
     {
         // Handle the case where there's no data. For now, let's return an empty view.
         // Ideally, you should inform the user that there's no data for the selected year.
         return View(new Chart()); // Assuming ChartViewModel is your view model for the chart view.
     }

     var groupedData = data
         .Where(h => !year.HasValue || h.UploadDate.Year == year.Value) // Apply the year filter only if year has a value.
         .GroupBy(h => h.UploadDate.Month)
         .Select(group => new
         {
             Month = group.Key,
             AverageCalories = group.Average(h => h.AverageCalories)
         })
         .OrderBy(x => x.Month)
         .ToList();

     // If groupedData is empty after filtering, provide a fallback or handle the case.
     if (!groupedData.Any())
     {
         // Handle the empty case as you see fit.
         return View(new Chart());
     }

     ViewData["Months"] = groupedData.Select(c => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(c.Month)).ToList();
     ViewData["AverageCalories"] = groupedData.Select(c => c.AverageCalories).ToList();

     return View(new Chart*//*

// Initialize your ChartViewModel with the necessary data.
// Add the necessary properties and initialization as per your ChartViewModel definition.





// Your Chart action method seems correct, but ensure that the data is not null
*//*public IActionResult Chart(int? year)
{
    var query = _context.History.AsQueryable();

    if (year.HasValue)
    {
        query = query.Where(h => h.UploadDate.Year == year.Value);
    }

    var groupedData = query
        .GroupBy(h => h.UploadDate.Month)
        .Select(group => new
        {
            Month = group.Key,
            AverageCalories = group.Average(h => h.AverageCalories)
        })
        .OrderBy(x => x.Month)
        .ToList();

    ViewData["Months"] = groupedData.Select(c => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(c.Month)).ToList();
    ViewData["AverageCalories"] = groupedData.Select(c => c.AverageCalories).ToList();

    return View();
}

}
}*/
/*using Microsoft.AspNetCore.Mvc;
using System.Linq;
using FYP5.Models; // Make sure this namespace points to where your DailyCalories model is located

namespace FYP5.Controllers
{
    public class ChartController : Controller
    {*/

/*    public IActionResult Line()
        {
            PrepareData(2);
            ViewData["Chart"] = "line";
            ViewData["Title"] = "Price Range";
            ViewData["ShowLegend"] = "false";
            return View("Chart");
        }


        private void PrepareData(int x)
        {

            int[] dataMax = new[] { 0, 0, 0, 0, 0 };
            List<History> list = DBUtl.GetList<History>("SELECT * FROM History ORDERED BY UploadDate");
            foreach (History cdt in list)
            {

                dataMax[CalcCal(cdt.AverageCalories)]++;
            }


            string[] user = new[] { "Calorie Count < 700", "Calorie Count > 700" };

            ViewData["Legend"] = "Healthy";
            // ViewData["Colors"] = colors;
            //ViewData["Labels"] = user;
            if (x == 2)
                ViewData["Data"] = dataMax;
        }

        private static int CalcCal(int c)
        {
            if (c > 700) return 1;

            else return 0;
        }
    }
}*/



/* public IActionResult Line()
     {
         PrepareData();
         ViewData["Title"] = "Calorie Count of Pictures Uploaded";
         return View("Chart");

     }

     private List<History> PrepareData()
     {

         // Example: Fetching the first or a specific AverageCalorie record
         string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
         List<History> historyList = DBUtl.GetList<History>("SELECT * FROM History ORDERED BY UploadDate");
         return historyList;
         // If a record is found, pass its value to the view
         *//*if (avgCalorieRecord != null)
             {
                 ViewData["AverageCalorieValue"] = avgCalorieRecord.CalorieValue; // Replace 'CalorieValue' with the actual property name
             }
             else
             {
                 ViewData["AverageCalorieValue"] = "No data available";
             }*//*

     }
 }
}*/
/* int[] dataMax = new[] { 0, 0, 0, 0, 0 };
 List<History> list = DBUtl.GetList<History>("SELECT * FROM History ORDERED BY Date");
 foreach (History cdt in list)
 {

     dataMax[CalcCal(cdt.AverageCalories)]++;
 }

 // Chart configurations
 *//*string[] colors = new[] { "green", "red" };
 string[] labels = new[] { "Calorie Count < 700", "Calorie Count > 700" };*//*

 ViewData["Legend"] = "Healthy";
 ViewData["Colors"] = "colors";
 ViewData["Labels"] = "user";
 if (x == 2)
     ViewData["Data"] = dataMax;
}
private int CalcCal(int c)
{
 if (c > 700) return 1;

 else return 0;
}
}
}

*/


/* var calorieData = FetchCalorieData();

 // Prepare chart data
 ViewBag.Labels = calorieData.Select(c => c.Gender).Distinct().ToArray();
 ViewBag.Data = calorieData.Select(c => c.AverageCalories).ToArray();
 ViewBag.BackgroundColors = new[] { "#FF6384", "#36A2EB" }; // Example colors
 ViewBag.BorderColors = new[] { "#FF6384", "#36A2EB" }; // Example border colors

 return View();*/

/* private void PrepareData(int x)
 {
     string[] dataAvg = new[] { 0, 0, 0, 0, 0 };
     List<UserHistory> List = DBUtl.GetList<UserHistory>("SELECT * FROM UserHistory");

 }
 private List<DailyCalories> FetchCalorieData()
 {
     // Mocked data. Replace this with your actual data access method.
     return new List<DailyCalories>
     {
         new DailyCalories { Gender = "Female", AverageCalories = 2000 },
         new DailyCalories { Gender = "Male", AverageCalories = 2500 }
         // Add more data as needed
     };
 }
}*/




/*using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FYP5.Models; // Assuming your Calories model is in this namespace

namespace FYP5.Controllers
{
    public class ChartController : Controller
    {
        public IActionResult Bar()
        {
            string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            // Use parameterized queries to prevent SQL injection
            string sql = "SELECT Gender, AVG(DailyCalories) AS AverageCalories FROM DailyCalories WHERE UserId = {0} GROUP BY Gender";
            List<DailyCalories> calorieData = DBUtl.GetList<DailyCalories>(sql, userid);

            // Assuming that calorieData already has the correct structure you need for the chart
            var labels = calorieData.Select(cd => cd.Gender).Distinct().ToArray();
            var data = calorieData.Select(cd => cd.AverageCalories).ToArray();

            // Set ViewData for the bar chart
            ViewData["ChartType"] = "bar";
            ViewData["Title"] = "Average Calorie Count by Gender";
            ViewData["ShowLegend"] = true; // Assuming the view expects a boolean
            ViewData["Legend"] = "Average Calories";
            ViewData["Colors"] = new string[] { "#007bff", "#ff6b81" }; // Use hex colors for clarity
            ViewData["Labels"] = labels;
            ViewData["Data"] = data;

            return View("Chart");
        }
    }
}*/

/*using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Security.Claims;

namespace FYP5.Controllers
{
    public class ChartController : Controller
    {
        public IActionResult Bar()
        {
            PrepareData(0);
            ViewData["Chart"] = "bar";
            ViewData["Title"] = "Average Calorie Count by Gender";
            ViewData["ShowLegend"] = "true";
            return View("Chart");
        }


        private void PrepareData(int x)
        {
            int[] totalCalories = new int[] { 0, 0, 0, 0, 0 };

            string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            // Example query using parameterized SQL
            List<Calories> calorieData = DBUtl.GetList<Calories>("SELECT Gender, AVG(DailyCalories) AS AverageCalories FROM DailyCalories WHERE UserId = '{0}' GROUP BY Gender", userid);


            // Set ViewData for bar chart
            ViewData["Chart"] = "bar";
            ViewData["Title"] = "Average Calorie Count by Gender";
            ViewData["ShowLegend"] = "true"; // Displaying legend for better understanding
            ViewData["Legend"] = "Average Calories";
            ViewData["Colors"] = new[] { "Blue", "Pink" }; // Example colors for two genders
            ViewData["Labels"] = "Genders";
            ViewData["Data"] = "AverageCalories";

        }
    }
}
       /* public IActionResult Bar(string userid)
        {
            PrepareData(0, userid);
        }
    }

        }








//List<JiakUser> list = DBUtl.GetList<JiakUser>/("SELECT * FROM JiakUser");

//foreach (JiakUser user in list)*/

