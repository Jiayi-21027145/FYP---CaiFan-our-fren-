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
    





