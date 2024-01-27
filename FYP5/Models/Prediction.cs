using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FYP5.Models;

[Owned]
public class BoundingBox
{
   
    public double Left { get; set; }

    public double Top { get; set; }

    public double Width { get; set; }

    public double Height { get; set; }
}

    public  class Prediction
{
    public int PredictionId { get; set; }

    public string? TagName { get; set; }

    public double? Probability { get; set; }

    public string? MenuId { get; set; }

    public BoundingBox ?Box { get; set; }

    public decimal? LowestPrice { get; set; }

    public decimal? HighestPrice { get; set; }
    [Column("HighestNv")]
    public int? HighestNv { get; set; }
    [Column("LowestNv")]
    public int? LowestNv { get; set; }
    [Column("AverageNv")]
    public int? AverageNv { get; set; }

    public int? DatasetId { get; set; }

    public virtual Dataset? Dataset { get; set; }

    public virtual Menu? Menu { get; set; }
}
