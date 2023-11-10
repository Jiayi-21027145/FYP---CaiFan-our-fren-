using System;
using System.Collections.Generic;

namespace FYP5.Models;

public partial class Dataset
{
    public int ImageId { get; set; }

    public string ImageName { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public string Label { get; set; } = null!;
}
