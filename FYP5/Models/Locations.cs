using System;
using System.Collections.Generic;

namespace FYP5.Models;

public partial class Locations
{
    public int LocationId { get; set; }

    public string LocationName { get; set; } = null!;

    public virtual ICollection<LocationPrice> LocationPrice { get; set; } = new List<LocationPrice>();
}
