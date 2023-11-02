using System;
using System.Collections.Generic;

namespace FYP2.Models;

public partial class ImageUploads
{
    public int UploadId { get; set; }

    public int? UserId { get; set; }

    public string ImageName { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public DateTime? UploadDate { get; set; }

    public virtual Users? User { get; set; }
}
