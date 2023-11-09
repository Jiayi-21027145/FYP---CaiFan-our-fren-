using Microsoft.AspNetCore.Mvc;

namespace FYP3.Controllers;

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

    [Authorize(Roles = "Admin")]
    public IActionResult SalesReport()
    {
        DbSet<UserHistory> dbsUHistory = _dbCtx.UserHistory;
        
/*
        ViewData["mugOrders"] = dbsMug.ToList<MugOrder>();
        ViewData["shirtOrders"] = dbsShirt.ToList<ShirtOrder>();*/


        return View();
    }



}
