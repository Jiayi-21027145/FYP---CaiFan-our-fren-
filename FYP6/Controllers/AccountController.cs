using Microsoft.AspNetCore.Mvc;

namespace FYP6.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
