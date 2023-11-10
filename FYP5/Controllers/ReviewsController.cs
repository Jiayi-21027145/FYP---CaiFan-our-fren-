using Microsoft.AspNetCore.Mvc;

using FYP5.Models;
using FYP5.Services;
using Microsoft.EntityFrameworkCore;

namespace FYP5.Controllers;

public class ReviewsController : Controller
{
    private readonly AppDbContext _dbCtx;

    public ReviewsController(AppDbContext dbCtx)
    {
        _dbCtx = dbCtx;
    }

    public IActionResult Index()
    {
        DbSet<Reviews> dbs = _dbCtx.Reviews;

        List<Reviews> model = dbs.ToList();

        return View("Reviews", model);

    }
}
