using Microsoft.AspNetCore.Mvc;
using System.Linq;
using FYP5.Models; 
using System.Globalization;

namespace FYP5.Controllers
{
    public class LineController : Controller

    {
        private readonly AppDbContext _dbCtx; 

        public LineController(AppDbContext dbCtx)
        {
            _dbCtx = dbCtx;
        }

            public IActionResult Index()
        {
            return View("Index");
        }
    }
}
