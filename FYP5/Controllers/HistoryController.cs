using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FYP5.Controllers
{
    public class HistoryController : Controller
    {
        private readonly IDbService _dbSvc;

        public HistoryController(IDbService dbSvc)
        {
            _dbSvc = dbSvc;
        }
        public IActionResult Index()
        {
            string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            string select = @"SELECT * FROM History WHERE UserId = '{0}'";
            List<History> list = DBUtl.GetList<History>(select, userid);

            foreach (var history in list)
            {
                List<string> dishes = new List<string> { history.DishOne, history.DishTwo, history.DishThree, history.DishFour, history.DishFive, history.DishSix };
                dishes.Sort((x, y) => CompareDishes(x, y));

                history.DishOne = dishes[0];
                history.DishTwo = dishes[1];
                history.DishThree = dishes[2];
                history.DishFour = dishes[3];
                history.DishFive = dishes[4];
                history.DishSix = dishes[5];
                // ... Assign the rest of the dishes
            }

            return View("Index", list);
        }

        private int CompareDishes(string x, string y)
        {
            string xType = GetDishType(x);
            string yType = GetDishType(y);

            if (xType == yType) return 0;
            if (xType == "rice") return -1; //-1 mean come before, 1 mean that it will come after the second dish
            if (yType == "rice") return 1;
            if (xType == "vegetable") return -1;
            if (yType == "vegetable") return 1;
            if (xType == "Meat") return -1;
            if (yType == "Meat") return 1;
            if (xType == "Fish") return -1;
            if (yType == "Fish") return 1;
            if (xType == "Egg") return -1;
            if (yType == "Egg") return 1;
            return 0;
        }

        private string GetDishType(string dish)
        {
            // Assuming dish names contain keywords that can identify their type
            if (dish.Contains("White Rice", StringComparison.OrdinalIgnoreCase))
            {
                return "rice";
            }
            else if (dish.Contains("NonLeafy", StringComparison.OrdinalIgnoreCase) ||
                     dish.Contains("Leafy", StringComparison.OrdinalIgnoreCase))
            {
                return "vegetable";
            }
            else if (dish.Contains("CrispyMeat", StringComparison.OrdinalIgnoreCase) ||
                     dish.Contains("CrispyMeatWSauce", StringComparison.OrdinalIgnoreCase) ||
                     dish.Contains("BraisedMeat", StringComparison.OrdinalIgnoreCase))
            {
                return "meat";
            }
            else if (dish.Contains("WhiteFish", StringComparison.OrdinalIgnoreCase) ||
                     dish.Contains("BatangFish", StringComparison.OrdinalIgnoreCase))
            {
                // Default category if none of the above conditions are met
                return "Fish";
            }
            else if (dish.Contains("SteamedEgg", StringComparison.OrdinalIgnoreCase) ||
                     dish.Contains("BoiledEgg", StringComparison.OrdinalIgnoreCase) ||
                     dish.Contains("Omelette", StringComparison.OrdinalIgnoreCase))
            {
                return "Egg";
            }
            else
            {
                return "Others";
            }

        }

        [Authorize]
        public IActionResult Summary(int ryear, int rmonth)
        {
            /* List<History> data = null!;*/
            string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            string select = @"SELECT * FROM History WHERE UserId = '{0}'";
            List<History> data = _dbSvc.GetList<History>(select, userid);
            ViewData["ryear"] = ryear;
            ViewData["rmonth"] = rmonth;

            if (ryear <= 0)
            {
               /* data = _dbSvc.GetList<History>(
                    @"SELECT * FROM History");*/
                ViewData["reportheader"] = "Overall View of Total Average Calories Intake and Spending Per Year";

                // Retrieve summary data grouped by Year
                var model = data
                    .GroupBy(b => b.UploadDate.Year)
                    .OrderByDescending(g => g.Key)
                    .Select(g => new
                    {
                        Group = g.Key,
                        Total = g.Sum(b => b.AverageCalories),
                        T = g.Sum(b => b.AveragePrice),
                    })
                    .ToExpandoList();

                return View(model);
            }
            else if (rmonth <= 0 || rmonth > 12)
            {
                string yearQuery = @"SELECT * FROM History WHERE YEAR(UploadDate) = {0} AND UserId = '{1}'";
                data = _dbSvc.GetList<History>(yearQuery, ryear, userid);

                ViewData["reportheader"] = $"Total Average Calories Intake and Spending {ryear} by Month";

                // Retrieve summary data grouped by Month for a given Year
                var model = data
                    .GroupBy(b => b.UploadDate.Month)
                    .OrderByDescending(g => g.Key)
                    .Select(g => new
                    {
                        Group = g.Key,
                        Total = g.Sum(b => b.AverageCalories),
                        T = g.Sum(b => b.AveragePrice),
                    })
                    .ToExpandoList();


                return View(model);
            }
            else
            {
                string monthQuery = @"SELECT * FROM History WHERE YEAR(UploadDate) = {0} AND MONTH(UploadDate) = {1} AND UserId = '{2}'";
                data = _dbSvc.GetList<History>(monthQuery, ryear, rmonth, userid);

                ViewData["reportheader"] = $"Average Calories Intake and Spending {ryear} Month {rmonth} by Day";

                // Retrieve summary data grouped by Day for a given Year/Month
                var model = data
                    .GroupBy(b => b.UploadDate.Day)
                    .OrderByDescending(g => g.Key)
                    .Select(g => new
                    {
                        Group = g.Key,
                        Total = g.Sum(b => b.AverageCalories),
                        T = g.Sum(b => b.AveragePrice),
                    })
                    .ToExpandoList();


                return View(model);
            }
        }
    }
}

       


