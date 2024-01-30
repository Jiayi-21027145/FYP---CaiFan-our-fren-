using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FYP5.Models
{
   public class LocationItemPriceViewModel
{
        public string LocationName { get; set; } = null!;
        public string ItemName { get; set; } = null!;
        public decimal? Price { get; set; }
}

}
