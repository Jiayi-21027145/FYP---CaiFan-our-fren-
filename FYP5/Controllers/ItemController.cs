using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using FYP5.Models; // Replace with your actual namespace

namespace FYP5.Controllers
{
    public class ItemController : Controller
    {
        private readonly AppDbContext _dbCtx;

        public ItemController(AppDbContext dbContext)
        {
            _dbCtx = dbContext;
        }

        public IActionResult Index()
        {
            // Replace '1' with the actual location ID you want to filter by
          /*  int locationIdToFilterBy = 1;

            var itemsAtLocation = _dbCtx.Items
         .Where(item => item.LocationPrice.Any(lp => lp.LocationId == locationIdToFilterBy))
         .Select(item => new SelectListItem
         {
             Value = item.ItemId.ToString(),
             Text = item.ItemName
         }).ToList();

            // Create a new SelectList with the items
            var itemSelectList = new SelectList(itemsAtLocation, "Value", "Text");

            // Create the view model and set the Items property to the SelectList
            var viewModel = new LocationPriceViewModel
            {
                Items = itemSelectList
            };

            return View(viewModel);*/
            return View();
        }
    }
}