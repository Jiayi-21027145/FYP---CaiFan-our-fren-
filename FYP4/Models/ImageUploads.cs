using System;
using System.Collections.Generic;

namespace FYP4.Models;

public partial class ImageUploads
{
    public int UploadId { get; set; }

    public string? UserId { get; set; }

    public string ImageName { get; set; } = null!;

    public byte[] ImageData { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public DateTime? UploadDate { get; set; }

    public virtual JiakUser? User { get; set; }
}
