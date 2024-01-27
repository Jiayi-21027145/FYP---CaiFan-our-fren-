using System;
using System.Collections.Generic;

namespace FYP5.Models;

public partial class Menu
{
    public string FoodName { get; set; } = null!;

    public string? ImageData { get; set; }

    public decimal? LowestPrice { get; set; }

    public decimal? HighestPrice { get; set; }

    public decimal? AveragePrice { get; set; }

    public string? NutrientName { get; set; }

    public int? HighestNv { get; set; }

    public int? LowestNv { get; set; }

    public int? AverageNv { get; set; }

    public virtual ICollection<Prediction> Prediction { get; set; } = new List<Prediction>();
}
