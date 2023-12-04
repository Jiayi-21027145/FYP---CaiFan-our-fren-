using System;
using System.Collections.Generic;

namespace FYP5.Models;

public partial class Menu
{
    public string ImageData { get; set; } = null!;

    public string FoodName { get; set; } = null!;

    public string FoodType { get; set; } = null!;

    public decimal LowestPrice { get; set; }

    public decimal HighestPrice { get; set; }

    public decimal AveragePrice { get; set; }

    public string NutrientName { get; set; } = null!;

    public int? HighestValue { get; set; }
    public int? LowestValue { get; set; }
    public int? AverageValue { get; set; }
}
