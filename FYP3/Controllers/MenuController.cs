using Microsoft.AspNetCore.Mvc;

using FYP3.Models;
using FYP3.Services;
using Microsoft.EntityFrameworkCore;
using System;

namespace FYP3.Controllers;

public class MenuController : Controller
{
    // TODO L03 TASK 1B: Create a private readonly variable _dbCtx of type AppDbContext class
    private readonly AppDbContext _dbCtx;

    // TODO L03 TASK 1C: Create constructor to receive dbContext and initialize the _dbCtx variable

    public MenuController(AppDbContext dbCtx) 
    {
        _dbCtx = dbCtx;
    }

    public IActionResult Index()
    {
        // TODO L03 TASK 1E: Set dbs to property dbSet for mug orders
        DbSet<Menu> dbs = _dbCtx.Menu;


        // TODO L03 TASK 1F: Set model to all records of mug orders - use dbs.ToList()
        List<Menu> model = dbs.ToList();

        // TODO L03 TASK 1H: Pass model to view
        return View("Menu", model); // this line needs to be modified
    }
}



