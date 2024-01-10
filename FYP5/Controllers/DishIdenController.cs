using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FYP5.Controllers
{
    public class DishIdenController : Controller
    {
        public IActionResult Index()
        {
 
            ViewData["UserId"] =
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            return View();
        }

    }
}
