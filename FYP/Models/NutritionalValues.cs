using System;
using System.Collections.Generic;

namespace FYP.Models;

public partial class NutritionalValues
{
    public int NutritionalValueId { get; set; }

    public int? FoodTypeId { get; set; }

    public string NutrientName { get; set; } = null!;

    public decimal Value { get; set; }

    public virtual FoodTypes? FoodType { get; set; }
}
