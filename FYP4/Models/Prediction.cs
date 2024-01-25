using System;
using System.Collections.Generic;

namespace Lesson05.Models;

public partial class Prediction
{
    public int PredictionId { get; set; }

    public string? TagName { get; set; }

    public double? Probability { get; set; }

    public string? MenuId { get; set; }

    public int? BoxId { get; set; }

    public decimal? LowestPrice { get; set; }

    public decimal? HighestPrice { get; set; }

    public int? Calories { get; set; }

    public int? DatasetId { get; set; }

    public virtual ICollection<BoundingBox> BoundingBox { get; set; } = new List<BoundingBox>();

    public virtual Dataset? Dataset { get; set; }

    public virtual Menu? Menu { get; set; }
}
