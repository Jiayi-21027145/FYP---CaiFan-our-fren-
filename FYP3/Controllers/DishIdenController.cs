using FYP3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FYP3.Controllers
{
    public class DishIdenController : Controller
    {
        private readonly ILogger<DishIdenController> _logger;

        public DishIdenController(ILogger<DishIdenController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}