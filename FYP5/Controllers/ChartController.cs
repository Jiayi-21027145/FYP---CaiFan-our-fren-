using Microsoft.AspNetCore.Mvc;
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
            ViewData["ShowLegend"] = "false";
            return View("Chart");
        }

       // public IActionResult Bar()
       // {
       //     PrepareData(1);
       //     ViewData["Chart"] = "bar";
       //     ViewData["Title"] = "Minimum Calories Intake Summary";
       //     ViewData["ShowLegend"] = "false";
       //      return View("Chart");
       // }

        private void PrepareData(int x)
        {
            int[] totalCalories = new int[] { 0, 0, 0, 0, 0 };

            string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            // Example query using parameterized SQL
            List<DailyCalories> calorieData = DBUtl.GetList<DailyCalories>("SELECT Gender, AVG(DailyCalories) AS AverageCalories FROM DailyCalories WHERE UserId = '{0}' GROUP BY Gender", userid);


            // Set ViewData for bar chart
            ViewData["Legend"] = "Calories Intake Range";
                    ViewData["Colors"] = new[] { "Blue", "Pink" }; // Colors for the diff genders
                    ViewData["Labels"] = "Gender";
                    ViewData["Data"] = "Average";
                    ViewData["ShowLegend"] = "true";
                    //ViewData["Data"] = totalIntake;

                }
            }
}



            

                //List<JiakUser> list = DBUtl.GetList<JiakUser>/("SELECT * FROM JiakUser");

                //foreach (JiakUser user in list)



