using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FYP5.Models;

public partial class Reviews
{
    public int ReviewId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }
    [NotMapped]
    [Required(ErrorMessage = "Please select Photo")]
    public IFormFile Photo { get; set; } = null!;

    public string ImageData { get; set; } = null!;

    public DateTime PublishDate { get; set; }
}
