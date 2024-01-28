using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using FYP5.Models; // Make sure this namespace points to where your History model is located
using System.Security.Claims;

namespace FYP5.Controllers
{
    public class ChartController : Controller
    {
        public IActionResult Line()
        {
            PrepareData();
            ViewData["Title"] = "Calorie Count of Pictures Uploaded";
            return View("Chart");
           
        }

        private void PrepareData()
        {
            // Fetch data from the database
            List<History> list = DBUtl.GetList<History>("SELECT * FROM History ORDERED BY Dates");
            string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            // Aggregate data
            int[] dataMax = new[] { 0, 0 };
            foreach (History cdt in list)
            {
                int index = CalcCal(cdt.AverageCalories);
                dataMax[index]++;
            }

            // Chart configurations
            /*string[] colors = new[] { "green", "red" };
            string[] labels = new[] { "Calorie Count < 700", "Calorie Count > 700" };*/

            // Assigning data to ViewData
            /* ViewData["Colors"] = colors;
             ViewData["Labels"] = labels;
             ViewData["Data"] = dataMax;
             ViewData["Legend"] = "Healthy";
         }*/

         private int CalcCal(int AverageCalories)
         {
             return 
         }
        }
        }
    }




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

