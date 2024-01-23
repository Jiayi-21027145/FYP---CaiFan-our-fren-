using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

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
            return View();
        }
    }
        
    

    
    
}

            
