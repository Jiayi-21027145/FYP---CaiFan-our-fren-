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
            int locationIdToFilterBy = 1;

            var itemsAtLocation = _dbCtx.Items
                .Where(item => item.LocationID == locationIdToFilterBy)
                .Select(item => new SelectListItem
                {
                    Value = item.ItemID.ToString(),
                    Text = item.Name
                })
                .ToList();

            var viewModel = new ItemLocationViewModel
            {
                Items = new SelectList(itemsAtLocation, "Value", "Text")
            };

            return View(viewModel);
        }
    }
}
