using System;
using System.Collections.Generic;

namespace FYP5.Models;

public partial class Prediction
{
    public int PredictionId { get; set; }

    public string? TagName { get; set; }

    public double? Probability { get; set; }

    public int? MenuId { get; set; }

    public decimal? LowestPrice { get; set; }

    public decimal? HighestPrice { get; set; }

    public int? HighestNv { get; set; }

    public int? LowestNv { get; set; }

    public int? AverageNv { get; set; }

    public int? DatasetId { get; set; }

    public virtual ICollection<BoundingBox> BoundingBox { get; set; } = new List<BoundingBox>();

    public virtual Dataset? Dataset { get; set; }

    public virtual Menu? Menu { get; set; }

    public virtual BoundingBox? Box { get; set; } // Navigation property
}
