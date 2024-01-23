using System;
using System.Collections.Generic;

namespace FYP5.Models;

public partial class UserHistory
{
    public int Id { get; set; }

    public string? UserId { get; set; }

    public DateOnly? Date { get; set; }
    public TimeOnly? Time { get; set; }

    public string? Location { get; set; }

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
    public string? Omelette { get; set; }
    public int MinimumCalories { get; set; }
    public int MaximumCalories { get; set; }
    public IFormFile Photo { get; set; } = null!;

    public string Image { get; set; } = null!;
}
