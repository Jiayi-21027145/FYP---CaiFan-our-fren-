using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace FYP5.Controllers
{
    public class ReviewsController : Controller
    {
       
        
        public IActionResult Index()
        {
            DataTable dt = DBUtl.GetTable("SELECT * FROM Reviews");
            return View("Index", dt.Rows);

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Reviews r)
        {
            ModelState.Remove("ImageData"); // No Need to Validate "Picture" - derived from "Photo".
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Invalid Input";
                ViewData["MsgType"] = "warning";
                return View("Create");
            }
            else
            {
                string picfilename = DoPhotoUpload(r.Photo);

                string sql = @"INSERT Reviews(Rating, Comment, ImageData) 
                           VALUES({0},'{1}','{2}')";

                string insert =
                   string.Format(sql, r.Rating, r.Comment,
                                     picfilename);
                if (DBUtl.ExecSQL(insert) == 1)
                {
                    TempData["Message"] = $"Review created Successfully";
                    TempData["MsgType"] = "success";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["Message"] = DBUtl.DB_Message;
                    ViewData["ExecSQL"] = DBUtl.DB_SQL;
                    ViewData["MsgType"] = "danger";
                    return View("Create");
                }
            }
        }
        [Authorize(Roles ="Users")]
        [HttpGet]
        public IActionResult Update(int id)
        {
            //string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            string select = @"SELECT * FROM Reviews  
                         WHERE ReviewId={0}";

            // TODO: Lesson09 Task 2c - Make insecure DB SELECT secure.
            string sql = string.Format(select, id);
            List<Reviews> r = DBUtl.GetList<Reviews>(select, id);
            if (r.Count == 1)
            {
                Reviews trip = r[0];
                return View(trip);
            }
            else
            {
                TempData["Message"] = "Trip Record does not exist";
                TempData["MsgType"] = "warning";
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Users")]
        [HttpPost]
        public IActionResult Update(Reviews r)
        {
            //ModelState.Remove("Photo");       // No Need to Validate "Photo"
            //ModelState.Remove("SubmittedBy"); // Ignore "SubmittedBy". See claim below.

            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Invalid Input";
                ViewData["Message"] = "Warning";
                return View("Update", r);
            }
            else
            {
                //string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

                string update = @"UPDATE Reviews   
                              SET Rating={1}, Comment='{2}', ImageData='{3}'
                              WHERE Id={0}'";
                //TODO: Lesson09 Task 2d - Make insecure DB UPDATE secure.
                string sql = string.Format(update, r.ReviewId,
                                          r.Rating, r.Comment, r.ImageData);
                if (DBUtl.ExecSQL(sql) == 1)
                {
                    TempData["Message"] = "Trip Updated";
                    TempData["MsgType"] = "success";
                }
                else
                {
                    TempData["Message"] = DBUtl.DB_Message;
                    ViewData["ExecSQL"] = DBUtl.DB_SQL;
                    TempData["MsgType"] = "danger";
                }
                return RedirectToAction("Index");
            }
        }
        private string DoPhotoUpload(IFormFile photo)
        {
            string fext = Path.GetExtension(photo.FileName);
            string uname = Guid.NewGuid().ToString();
            string fname = uname + fext;
            string fullpath = Path.Combine(_env.WebRootPath, "reviews/" + fname);
            using (FileStream fs = new(fullpath, FileMode.Create))
            {
                photo.CopyTo(fs);
            }
            return fname;
        }

        private readonly IWebHostEnvironment _env;
        public ReviewsController(IWebHostEnvironment environment)
        {
            _env = environment;
        }
    }

}
    





