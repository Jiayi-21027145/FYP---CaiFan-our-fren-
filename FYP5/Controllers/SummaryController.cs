using Microsoft.AspNetCore.Mvc;
using FYP5.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FYP5.Controllers
{
    public class SummaryController : Controller
    {
        private readonly AppDbContext _dbCtx;

        public SummaryController(AppDbContext dbContext)
        {
            _dbCtx = dbContext;
        }

        public IActionResult UserIndex()
        {
            var items = _dbCtx.Items.ToList();
            var locations = _dbCtx.Locations.ToList(); // Assuming you have a DbSet<Location> Locations
            var viewModel = new UserIndexViewModel();
            {
                var location = location.Select(lo=> new 
                {
                    LoName = "Location Name"

                }
                
                                   
                Item = items;
                Location = locations;
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CalculatePrice(int[] itemIds)
        {
            // Get the prices for the selected items in each location
            var prices = _dbCtx.LocationPrice
                .Include(lp => lp.Location)
                .Where(lp => itemIds.Contains(lp.ItemID))
                .ToList();

            // Calculate total price per location
            var totalsByLocation = prices
                .GroupBy(lp => lp.Location)
                .Select(group => new LocationTotal
                {
                    LocationName = group.Key.LocationName,
                    TotalPrice = group.Sum(lp => lp.Price)
                })
                .ToList();

            return View("Summary", totalsByLocation);
        }
    }
}
