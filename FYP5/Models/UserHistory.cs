using System;
using System.Collections.Generic;

namespace FYP5.Models;

public partial class UserHistory
{
    public string UserId { get; set; } = null!;

    public int Id { get; set; }

    public string? WhiteRice { get; set; }
    public string? CrispyMeatWSauce { get; set; }
    public string? CrispyMeat { get; set; }
    public string? BraisedMeat { get; set; }
    public string? NonLeafy { get; set; }
    public string? Leafy { get; set; }
    public string? WhiteFish { get; set; }
    public string? BatangFish { get; set; }
    public string? SteamedEgg { get; set; }
    public string? BoiledEgg { get; set; }
    public string Omelette { get; set; } = null!;

    public int MinimumCalories { get; set; }

    public int MaximumCalories { get; set; }
    public double LowestPrice { get; set; }

    public double HighestPrice { get; set; }

    public string Image { get; set; } = null!;

    public virtual JiakUser User { get; set; } = null!;
}