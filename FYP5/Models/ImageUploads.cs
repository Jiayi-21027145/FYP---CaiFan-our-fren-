using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FYP5.Models;

public partial class ImageUploads
{
    public int UploadId { get; set; }

    public string? UserId { get; set; }
    [Required(ErrorMessage = "Please enter Date/Time")]
    [DataType(DataType.DateTime)]
    [Remote(action: "VerifyDate", controller: "Performance")]
    public DateTime ImageDt { get; set; }
    public string ImageLc { get; set; } = null!;

    public string ImageName { get; set; } = null!;

    public byte[] ImageData { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public virtual JiakUser? User { get; set; }
}
