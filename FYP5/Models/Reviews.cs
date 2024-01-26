﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FYP5.Models;

public class Reviews
{
    public int? ReviewId { get; set;}

    public int? Rating { get; set; }

    public string? Comment { get; set; }
    [NotMapped]
    [ValidateNever]
    [Required(ErrorMessage = "Please select Photo")]
    public IFormFile Photo { get; set; } = null!;

    public string ImageData { get; set; } = null!;

    public DateTime PublishDate { get; set; } 
}
