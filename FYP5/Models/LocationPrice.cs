using System;
using System.Collections.Generic;

namespace FYP5.Models;

public partial class LocationPrice
{
    public int LocationId { get; set; }

    public int ItemId { get; set; }

    public decimal? Price { get; set; }

    public virtual Items Item { get; set; } = null!;

    public virtual Locations Location { get; set; } = null!;
}
