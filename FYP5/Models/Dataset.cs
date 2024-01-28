using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FYP5.Models;

public partial class Dataset
{
    public int ImageId { get; set; }

    public string ImageName { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public string DishName { get; set; } = null!;

    public double Probability { get; set; }
    [Required]
    public string Location { get; set; } = null!;
    [Required(ErrorMessage = "Please enter Date/Time")]
    [DataType(DataType.DateTime)]
    [Remote(action: "VerifyDate", controller: "DishIden")]
    public DateTime DateTime { get; set; }
    [NotMapped]
    [Required(ErrorMessage = "Please select Photo")]
    public IFormFile Photo { get; set; } = null!;

    public string? UserId { get; set; }

    public virtual JiakUser? User { get; set; }

    public virtual ICollection<Prediction> Prediction { get; set; } = new List<Prediction>();
}