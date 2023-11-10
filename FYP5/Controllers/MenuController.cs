using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

//using FYP5.Models;
// FYP5.Services;
//using Microsoft.EntityFrameworkCore;
//using System;

namespace FYP5.Controllers;

public class MenuController : Controller
{
    // TODO L03 TASK 1B: Create a private readonly variable _dbCtx of type AppDbContext class
    //private readonly AppDbContext _dbCtx;

    // TODO L03 TASK 1C: Create constructor to receive dbContext and initialize the _dbCtx variable

    //public MenuController(AppDbContext dbCtx) 
    //{
    //    _dbCtx = dbCtx;
    //}

    public IActionResult Index()
    {
        List<Menu> list = DBUtl.GetList<Menu>("SELECT * FROM Menu");
        return View("Index", list);
    }
    public IActionResult Display(string FoodName)
    {
        string sql = @"SELECT * FROM Menu
                                     WHERE Name = '{0}'";
        List<Menu> list = DBUtl.GetList<Menu>(sql, FoodName);
        if (list.Count == 1)
        {
            return View(list);
        }
        else
        {
            TempData["Message"] = "Menu not found";
            TempData["MsgType"] = "warning";
            return RedirectToAction("Index");
        }
    }
    
        // TODO L03 TASK 1E: Set dbs to property dbSet for mug orders
        // DbSet<Menu> dbs = _dbCtx.Menu;


        // TODO L03 TASK 1F: Set model to all records of mug orders - use dbs.ToList()
        //List<Menu> model = dbs.ToList();

        // TODO L03 TASK 1H: Pass model to view
        // View("Menu", model); // this line needs to be modified
    }




