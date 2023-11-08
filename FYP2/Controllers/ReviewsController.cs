using Microsoft.AspNetCore.Mvc;

using FYP2.Models;
using FYP2.Services;
using Microsoft.EntityFrameworkCore;

namespace FYP2.Controllers;

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
