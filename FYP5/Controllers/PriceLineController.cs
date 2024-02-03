using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using FYP5.Models;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

public class PLineController : Controller
{
    private readonly AppDbContext _context;

    public PLineController(AppDbContext context)
    {
        _context = context;
    }

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
                AveragePrice = group.Sum(h => h.AveragePrice)
            })
            .OrderBy(x => x.MonthNumber) // Ensure months are in order
            .ToList();

        // Pass the processed data to the view
        ViewBag.MonthlyData = monthlyData.Select(x => new { x.MonthName, x.AveragePrice }).ToList();

        // Return the view
        return View();
    }


}
