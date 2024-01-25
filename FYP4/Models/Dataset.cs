using System;
using System.Collections.Generic;

namespace Lesson05.Models;

public partial class Dataset
{
    public int ImageId { get; set; }

    public string ImageName { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public string DishName { get; set; } = null!;

    public double Probability { get; set; }

    public string Location { get; set; } = null!;

    public DateTime DateTime { get; set; }

    public virtual ICollection<Prediction> Prediction { get; set; } = new List<Prediction>();
}
