using Microsoft.AspNetCore.Mvc;

namespace FYP5.Controllers
{
    public class MenuController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public MenuController(IWebHostEnvironment environment)
        {
            _env = environment;
        }
        public IActionResult Index()
        {
            DataTable dt = DBUtl.GetTable("SELECT * FROM Menu");
            return View("Menu", dt.Rows);
        }
    }
}
