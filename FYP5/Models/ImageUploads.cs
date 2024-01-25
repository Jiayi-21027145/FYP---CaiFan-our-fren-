using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FYP5.Models;

public partial class ImageUploads
{
    public int UploadId { get; set; }
    [Required]
    public string? UserId { get; set; }
    [Required(ErrorMessage = "Please enter Date/Time")]
    [DataType(DataType.DateTime)]
    [Remote(action: "VerifyDate", controller: "DishIden")]
    public DateTime ImageDt { get; set; }
    [Required]
    public string ImageLc { get; set; } = null!;
    [Required]
    [NotMapped]
    public IFormFile Photo { get; set; } = null!;

    public string ImageName { get; set; } = null!;

    public byte[] ImageData { get; set; } = null!;

    public virtual JiakUser? User { get; set; }
}
