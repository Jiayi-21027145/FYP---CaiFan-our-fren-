using System;
using System.Collections.Generic;

namespace FYP5.Models;

public partial class Items
{
    public int ItemId { get; set; }

    public string ItemName { get; set; } = null!;

    public virtual ICollection<LocationPrice> LocationPrice { get; set; } = new List<LocationPrice>();
}
