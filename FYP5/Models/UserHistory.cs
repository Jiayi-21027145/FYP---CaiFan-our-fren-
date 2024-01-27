using System;
using System.Collections.Generic;

namespace FYP5.Models;

public partial class UserHistory
{
    public string UserId { get; set; } = null!;

    public int Id { get; set; }

    public string WhiteRice { get; set; } = null!;

    public string CrispyMeatWsauce { get; set; } = null!;

    public string CrispyMeat { get; set; } = null!;

    public string BraisedMeat { get; set; } = null!;

    public string NonLeafy { get; set; } = null!;

    public string Leafy { get; set; } = null!;

    public string WhiteFish { get; set; } = null!;

    public string BatangFish { get; set; } = null!;

    public string SteamedEgg { get; set; } = null!;

    public string BoiledEgg { get; set; } = null!;

    public string Omelette { get; set; } = null!;

    public int MinimumCalories { get; set; }

    public int MaximumCalories { get; set; }
    public double LowestPrice { get; set; }

    public double HighestPrice { get; set; }

    public string Image { get; set; } = null!;

    public virtual JiakUser User { get; set; } = null!;
}
