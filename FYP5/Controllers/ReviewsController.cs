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

                string insert = @"INSERT INTO Reviews(Rating, Comment, ImageData) 
                           VALUES({0}, '{1}', '{2}')";

                string sql =
                   string.Format(insert,r.Rating, r.Comment,
                                     picfilename);
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
                return View("Update", r);
            }
            else
            {
                TempData["Message"] = "Review Record does not exist";
                TempData["MsgType"] = "Warning";
                return RedirectToAction("Index");
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult Update(Reviews r)
        {
            ModelState.Remove("ImageData");       // No Need to Validate "Photo".

            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Invalid Input";
                ViewData["MsgType"] = "warning";   
                return View("Update", r);
            }
            else
            {
                //string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
 
                string update = @"UPDATE Reviews  
                              SET Rating={1}, Comment='{2}'
                              WHERE ReviewID={0}";

                string sql = string.Format(update, r.ReviewId, r.Rating, r.Comment);
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
        public IActionResult Delete(int id)
        {
            //string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            string select = @"SELECT * FROM Reviews
                         WHERE ReviewID={0}";

            string sql = string.Format(select, id);
            DataTable ds = DBUtl.GetTable(sql);
            if (ds.Rows.Count != 1)
            {
                TempData["Message"] = "Review Record does not exist";
                TempData["MsgType"] = "warning";
            }
            else
            {
                string photoFile = ds.Rows[0]["ImageData"]!.ToString()!;
                string fullpath = Path.Combine(_env.WebRootPath, "reviews/" + photoFile);
                System.IO.File.Delete(fullpath);

                string delete = "DELETE FROM Reviews WHERE ReviewID={0}";
                //TODO: Lesson09 Task 2f - Make insecure DB DELETE secure.
                string sql2 = string.Format(delete, id);
                int res = DBUtl.ExecSQL(sql2);
                if (res == 1)
                {
                    TempData["Message"] = "Review Record Deleted";
                    TempData["MsgType"] = "success";
                }
                else
                {
                    TempData["Message"] = DBUtl.DB_Message;
                    TempData["ExecSQL"] = DBUtl.DB_SQL;
                    TempData["MsgType"] = "danger";
                }
            }
            return RedirectToAction("Index");
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
    





