using System;
using System.Collections.Generic;

namespace Lesson05.Models;

public partial class BoundingBox
{
    public int BoxId { get; set; }

    public double Left { get; set; }

    public double Top { get; set; }

    public double Width { get; set; }

    public double Height { get; set; }

    public int? PredictionId { get; set; }

    public virtual Prediction? Prediction { get; set; }
}
