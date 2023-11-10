using System;
using System.Collections.Generic;

namespace FYP5.Models;

public partial class Reviews
{
    public int ReviewId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public byte[] ImageData { get; set; } = null!;
}
