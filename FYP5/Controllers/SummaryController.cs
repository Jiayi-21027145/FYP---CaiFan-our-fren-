using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FYP5.Controllers
{
    public class SummaryController : Controller
    {
        public IActionResult AdminIndex()
        {
            return View();
        }
        public IActionResult UserIndex ()
        {
            return View();
        }
    }
}
