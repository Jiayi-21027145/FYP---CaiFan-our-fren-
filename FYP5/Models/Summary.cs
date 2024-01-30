using System;
using System.Collections.Generic;

namespace FYP5.Models;

public partial class Summary
{
    public string UserId { get; set; } = null!;

    public int TotalCalories { get; set; }

    public int TotalCount { get; set; }

    public int TotalPic { get; set; }
    public string Item { get; set; } = null!;
    public string Location { get; set; } = null!;
}


