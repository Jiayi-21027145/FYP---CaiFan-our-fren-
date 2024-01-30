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

        [Authorize(Roles = "User")]
        public IActionResult UserIndex()
        {
            // Your existing code to fetch items and locations...
            ViewData["items"] = _dbCtx.Items.ToList<Items>();
            ViewData["locations"] = _dbCtx.Locations.ToList<Locations>();
            ViewData["users"] = _dbCtx.JiakUser.ToList<JiakUser>();
            /* var items = _dbCtx.Items.ToList();
             var locations = _dbCtx.Location.ToList();

             var viewModel = new LocationPrice
             {
                 Item = items,
                 Location = locations
             };
 */
            return View();
        }
        public IActionResult Summary(int itemId, int locationId)
        {
            var result = _dbCtx.LocationPrice
                .Where(lp => lp.ItemId == itemId && lp.LocationId == locationId)
                .Include(lp => lp.Location) // Include to load related Location
                .Include(lp => lp.Item)     // Include to load related Item
                .Select(lp => new
                {
                    LocationName = lp.Location.LocationName, // Accessing the related Location's Name
                    ItemName = lp.Item.ItemName,             // Accessing the related Item's Name
                    Price = lp.Price
                })
                .FirstOrDefault();

            if (result == null)
            {
                // Handle the case when no data is found
                return NotFound();
            }

            // Create a view model or use an anonymous type to pass the data to the view
            var viewModel = new LocationItemPriceViewModel
            {
                LocationName = result.LocationName,
                ItemName = result.ItemName,
                Price = result.Price
            };

            return View(viewModel);
            /* var result = _dbCtx.LocationPrice
                 .Where(lp => lp.ItemId == itemId && lp.LocationId == locationId)
                 .Include(lp => lp.Location)
                 .Include(lp => lp.Item)
                 .Select(lp => new
                 {
                     LocationName = lp.Location.LocationName,
                     ItemName = lp.Item.ItemName,
                     Price = lp.Price
                 })
                 .FirstOrDefault();*/
            /*
                        var viewModel = _dbCtx.LocationPrice
                .Where(lp => lp.ItemId == itemId && lp.LocationId == locationId)
                .Include(lp => lp.Location)
                .Include(lp => lp.Item)
                .Select(lp => new LocationItemPriceViewModel
                {
                    LocationName = lp.Location.LocationName,
                    ItemName = lp.Item.ItemName,
                    Price = lp.Price
                })
                .FirstOrDefault();


                        if (viewModel == null)
                        {
                            // Handle the case when the item or location is not found
                            return NotFound();
                        }

                        return View(viewModel);*/
        }

        /* [HttpPost]
         public IActionResult CalculatePrice(int[] itemIds)
         {
             // Get the prices for the selected items in each location
             var prices = _dbCtx.LocationPrice
                 .Include(lp => lp.LocationId)
                 .Where(lp => itemIds.Contains(lp.ItemId))
                 .ToList();

             // Calculate total price per location
             var totalsByLocation = prices
                 .GroupBy(lp => lp.LocationId)
                 .Select(group => new LocationTotal
                 {
                     LocationName = group.Key,
                     TotalPrice = group.Sum(lp => lp.Price ?? 0)
                 })
                 .ToList();

             return View("Summary", totalsByLocation);
         }*/
    }
}
