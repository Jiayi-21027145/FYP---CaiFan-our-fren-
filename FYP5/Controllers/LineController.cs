﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using FYP5.Models;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

public class LineController : Controller
{
    private readonly AppDbContext _context;

    public LineController(AppDbContext context)
    {
        _context = context;
    }

    /*public IActionResult Index(int? year)
    {
        // Retrieve distinct years from the History records in the database
        var yearsInDb = _context.History
            .Select(h => h.UploadDate.Year)
            .Distinct()
            .OrderByDescending(y => y) // You can order by descending to have the most recent year first
            .ToList();

        // Create a SelectList for the dropdown list
        ViewBag.Years = new SelectList(yearsInDb);

        // If a year is selected, filter the data; otherwise, return all records
        var filteredData = year.HasValue
            ? _context.History.Where(h => h.UploadDate.Year == year.Value)
            : _context.History;

        // Process the filtered data to get the average calories by month
        var monthlyData = filteredData
            .AsEnumerable() // This is important to process the grouping in-memory
            .GroupBy(h => h.UploadDate.Month)
            .Select(group => new
            {
                Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(group.Key),
                AverageCalories = group.Average(h => h.AverageCalories)
            })
            .OrderBy(x => x.Month)
            .ToList();

        ViewBag.MonthlyData = monthlyData;

        // Keep the selected year to re-select it in the dropdown when the page reloads
        ViewBag.SelectedYear = year;

        return View();
    }*/

    public IActionResult Index(int? year)
    {
        // Assuming you have the user's ID from the User Principal
        string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        // Filter histories by the logged-in user's ID
        var userHistories = _context.History
            .Where(h => h.UserId == userid);

        // Retrieve distinct years from the user's History records in the database and create a SelectList
        var yearsInDb = userHistories
            .AsNoTracking() // Use AsNoTracking for a read-only query, it's more efficient
            .Select(h => h.UploadDate.Year)
            .Distinct()
            .OrderByDescending(y => y)
            .ToList();

        ViewBag.Years = new SelectList(yearsInDb);

        // Set the selected year for the dropdown
        ViewBag.SelectedYear = year;

        // Filter the data based on the selected year, if it's been set
        if (year.HasValue)
        {
            userHistories = userHistories.Where(h => h.UploadDate.Year == year.Value);
        }

        // Materialize the query and process the data to get the average calories by month
        var monthlyData = userHistories
            .ToList() // Materialize the query to work with it in-memory
            .GroupBy(h => h.UploadDate.Month)
            .Select(group => new
            {
                MonthNumber = group.Key,
                MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(group.Key),
                AverageCalories = group.Sum(h => h.AverageCalories)
            })
            .OrderBy(x => x.MonthNumber) // Ensure months are in order
            .ToList();

        // Pass the processed data to the view
        ViewBag.MonthlyData = monthlyData.Select(x => new { x.MonthName, x.AverageCalories }).ToList();

        // Return the view
        return View();
    }


}
