using System;
using System.Collections.Generic;

namespace FYP6.Models;

public partial class Menu
{
    public string ImageData { get; set; } = null!;

    public string FoodName { get; set; } = null!;

    public decimal LowestPrice { get; set; }

    public decimal HighestPrice { get; set; }

    public decimal AveragePrice { get; set; }

    public string NutrientName { get; set; } = null!;

    public int HighestNv { get; set; }

    public int LowestNv { get; set; }

    public int AverageNv { get; set; }
}
