using System;
using System.Collections.Generic;

namespace FYP.Models;

public partial class Reviews
{
    public int ReviewId { get; set; }

    public int? UserId { get; set; }

    public int? FoodTypeId { get; set; }

    public int Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime ReviewDate { get; set; }

    public virtual FoodTypes? FoodType { get; set; }

    public virtual Users? User { get; set; }
}
