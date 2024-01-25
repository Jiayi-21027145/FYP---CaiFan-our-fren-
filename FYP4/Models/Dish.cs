using System;
using System.Collections.Generic;

namespace Lesson05.Models;

public partial class Dish
{
    public int FoodId { get; set; }

    public string Name { get; set; } = null!;

    public int HighestNv { get; set; }

    public int LowestNv { get; set; }
}
