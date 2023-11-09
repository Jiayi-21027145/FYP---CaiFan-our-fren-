using System;
using System.Collections.Generic;

namespace FYP3.Models;

public partial class Menu
{
    public byte[] ImageData { get; set; } = null!;

    public string FoodName { get; set; } = null!;

    public decimal Price { get; set; }

    public string NutrientName { get; set; } = null!;

    public string FoodType { get; set; } = null!;

    public int? NutritionalValue { get; set; }

    public decimal SellingPrice { get; set; }
}
