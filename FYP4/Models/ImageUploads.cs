using System;
using System.Collections.Generic;

namespace Lesson05.Models;

public partial class ImageUploads
{
    public int UploadId { get; set; }

    public string? UserId { get; set; }

    public string ImageLc { get; set; } = null!;

    public DateTime ImageDt { get; set; }

    public string ImageData { get; set; } = null!;

    public virtual JiakUser? User { get; set; }
}
