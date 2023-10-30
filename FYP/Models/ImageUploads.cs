using System;
using System.Collections.Generic;

namespace FYP.Models;

public partial class ImageUploads
{
    public int ImageId { get; set; }

    public int? UserId { get; set; }

    public int? FoodTypeId { get; set; }

    public string ImageName { get; set; } = null!;

    public byte[] ImageData { get; set; } = null!;

    public DateTime UploadDate { get; set; }

    public virtual FoodTypes? FoodType { get; set; }

    public virtual Users? User { get; set; }
}
