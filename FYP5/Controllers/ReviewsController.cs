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

                string insert = @"INSERT INTO Reviews(Rating, Comment, ImageData, PublishDate) 
                           VALUES({0}, '{1}', '{2}', '{3:dd MMMM yyyy}')";

                string sql =
                   string.Format(insert, r.Rating, r.Comment,
                                     picfilename, r.PublishDate);
                if (DBUtl.ExecSQL(sql) == 1)
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
        //[Authorize(Roles ="Users")]
        [HttpGet]
        public IActionResult Update(int id)
        {
            //string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            string select = @"SELECT * FROM Reviews 
                         WHERE ReviewID={0}";

            string sql = string.Format(select, id);
            List<Reviews> list = DBUtl.GetList<Reviews>(sql);
            if (list.Count == 1)
            {
                Reviews r = list[0];
                return View(r);
            }
            else
            {
                TempData["Message"] = "Review Record does not exist";
                TempData["MsgType"] = "Warning";
                return RedirectToAction("Index");
            }
        }
        
        [HttpPost]
        public IActionResult Update(Reviews r)
        {
            ModelState.Remove("Photo");       // No Need to Validate "Photo".

            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Invalid Input";
                ViewData["MsgType"] = "Warning";   
                return View("Update", r);
            }
            else
            {
                //string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

                string update = @"UPDATE Reviews   
                              SET Rating={1}, Comment='{2}' 
                              WHERE ReviewID={0}";
       
                string sql = string.Format(update, r.ReviewId,
                                          r.Rating, r.Comment);
                if (DBUtl.ExecSQL(sql) == 1)
                {
                    TempData["Message"] = "Review Updated";
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
    





