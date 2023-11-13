using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace FYP5.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public ReviewsController(IWebHostEnvironment environment)
        {
            _env = environment;
        }
        private string UploadFile(IFormFile ufile, string fname)
        {
            string fullpath = Path.Combine(_env.WebRootPath, fname);
            using (FileStream fs = new(fullpath, FileMode.Create))
            {
                ufile.CopyToAsync(fs);
            }
            return fname;
        }
        public IActionResult Review()
        {
            List<Reviews> list = DBUtl.GetList<Reviews>("SELECT * FROM Reviews");
            return View(list);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Reviews r, IFormFile photo)
        {
            if (!ModelState.IsValid)
            {
                return View(r);
            }
            else
            {
                r.ImageData = Path.GetFileName(photo.FileName);
                string fname = "reviews/" + r.ImageData;
                UploadFile(photo, fname);

                string sql = @"INSERT Reviews(ReviewID, Rating, 
                                            Comment, ImageData) 
                           VALUES({0},{1},'{2}','{3}')";

                string insert =
                   string.Format(sql, r.ReviewId, r.Rating, r.Comment,
                                      r.ImageData);
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
                    return View(r);
                }
            }
        }
    }

}
    





