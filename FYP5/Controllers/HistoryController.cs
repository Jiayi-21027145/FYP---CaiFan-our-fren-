using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace FYP5.Controllers
{
    public class HistoryController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            DataTable dt = DBUtl.GetTable("SELECT * FROM UserHistory");
            return View("Index", dt.Rows);

        }
    }
    
}
