/*using Microsoft.AspNetCore.Mvc;
using FYP5.Models;
using System.Linq;
using System.Security.Claims;
using RP.SOI.DotNet.Utils; // Assuming DBUtl is in this namespace

namespace FYP5.Controllers
{
    public class HistoryController : Controller
    {
        // [Authorize(Roles ="User, Admin")]
        public IActionResult Index()
        {

            string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            string select = @"SELECT * FROM UserHistory 
                          WHERE UserId = '{0}'";
            List<UserHistory> list = DBUtl.GetList<UserHistory>(select, userid);
            return View("Index", list);
        }

        public IActionResult Chart()
        {
            string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            // This is your SQL query that groups the data by gender and calculates the average calories.
            // You need to replace 'YourTableName' with the actual name of your table.
            string sql = @"
                SELECT Gender, AVG(AverageCalories) AS AverageCalories
                FROM YourTableName
                WHERE UserId = @0
                GROUP BY Gender";

            // Execute the query using the DBUtl class.
            List<DailyCalories> data = DBUtl.GetList<DailyCalories>(sql, userid);

            // Prepare the data for the Chart.js
            var chartData = new
            {
                Labels = data.Select(x => x.Gender).Distinct().ToArray(),
                Data = data.Select(x => x.AverageCalories).ToArray()
            };

            // Pass the data to the view
            return View(chartData);
        }
    }
}



    


*//*using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Net.Http;
using System.Threading.Tasks;
using FYP5.Models; // Assuming this namespace contains your model classes
using Newtonsoft.Json;

namespace FYP5.Controllers
{
    public class HistoryController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public HistoryController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:44328/userhistory/index");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<UserHistory>>(content);
                return View("Index", list);
            }
            else
            {
                // Handle error, e.g., return an error view
                return View("Error");
            }
        }

        public async Task<IActionResult> UserCaloriesChart()
        {
            string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"https://api.example.com/caloriesdata/{userid}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var caloriesData = JsonConvert.DeserializeObject<List<DailyCalories>>(content);

                var groupedData = caloriesData.GroupBy(c => c.Gender)
                                              .Select(g => new {
                                                  Gender = g.Key,
                                                  AverageCalories = g.Average(c => c.AverageCalories)
                                              }).ToList();

                var labels = groupedData.Select(g => g.Gender).ToArray();
                var data = groupedData.Select(g => g.AverageCalories).ToArray();

                ViewBag.Labels = labels;
                ViewBag.Data = data;

                return View("UserCaloriesChart");
            }
            else
            {
                // Handle error
                return View("Error");
            }
        }
    }
}*/

