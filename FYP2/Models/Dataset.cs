using System;
using System.Collections.Generic;

namespace FYP2.Models;

public partial class Dataset
{
    public int ImageId { get; set; }

    public string ImageName { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public string Label { get; set; } = null!;

    public virtual ICollection<Reviews> Reviews { get; set; } = new List<Reviews>();
}
