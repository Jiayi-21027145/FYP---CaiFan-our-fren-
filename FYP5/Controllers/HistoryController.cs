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

            /*// Create a new UserHistory object and populate it with necessary data
       var History = new History
       {
           Image = set.ImageName,
           UserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value, // If you store user ID in UserHistory                                                             // Other properties like PredictionResults, UploadDate, etc.
       };

       // Add the record to the UserHistory DbSet and save changes
       _dbCtx.History.Add(userHistory);
       await _dbCtx.SaveChangesAsync()*/
            ;
        }
    }
    
}
