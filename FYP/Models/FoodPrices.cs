using System;
using System.Collections.Generic;

namespace FYP.Models;

public partial class FoodPrices
{
    public int FoodPriceId { get; set; }

    public int? FoodTypeId { get; set; }

    public decimal Price { get; set; }

    public virtual FoodTypes? FoodType { get; set; }
}
