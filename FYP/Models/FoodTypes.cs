using System;
using System.Collections.Generic;

namespace FYP.Models;

public partial class FoodTypes
{
    public int FoodTypeId { get; set; }

    public string FoodTypeName { get; set; } = null!;

    public virtual ICollection<FoodPrices> FoodPrices { get; set; } = new List<FoodPrices>();

    public virtual ICollection<ImageUploads> ImageUploads { get; set; } = new List<ImageUploads>();

    public virtual ICollection<NutritionalValues> NutritionalValues { get; set; } = new List<NutritionalValues>();

    public virtual ICollection<Reviews> Reviews { get; set; } = new List<Reviews>();
}
