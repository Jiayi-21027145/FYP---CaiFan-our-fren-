using FYP.Services;
using Microsoft.AspNetCore.Mvc;

namespace FYP.Controllers;
public class GiftShopController : Controller
{
    private readonly AppDbContext _dbCtx;

    public GiftShopController(AppDbContext dbContext)
    {
        _dbCtx = dbContext;
    }

    public IActionResult Index()
    {
        return View();
    }
}



