using System;
using System.Collections.Generic;

namespace Lesson05.Models;

public partial class Reviews
{
    public int ReviewId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public string ImageData { get; set; } = null!;

    public DateTime PublishDate { get; set; }
}
