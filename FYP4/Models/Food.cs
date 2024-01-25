using System;
using System.Collections.Generic;

namespace Lesson05.Models;

public partial class Food
{
    public string UserId { get; set; } = null!;

    public DateTime Date { get; set; }

    public string FoodOne { get; set; } = null!;

    public string FoodTwo { get; set; } = null!;

    public string FoodThree { get; set; } = null!;

    public string FoodFour { get; set; } = null!;

    public string FoodFive { get; set; } = null!;

    public string FoodSix { get; set; } = null!;
}
