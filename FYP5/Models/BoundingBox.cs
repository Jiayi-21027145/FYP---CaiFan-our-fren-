using System;
using System.Collections.Generic;

namespace FYP5.Models;

public partial class BoundingBox
{
    public int BoundingBoxId { get; set; }

    public double Lefts { get; set; }

    public double Tops { get; set; }

    public double Width { get; set; }

    public double Height { get; set; }

    public int? PredictionId { get; set; }

    public virtual Prediction? Prediction { get; set; }
}
