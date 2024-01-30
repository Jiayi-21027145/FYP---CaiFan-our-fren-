using System.ComponentModel.DataAnnotations;

namespace FYP5.Models;

public class Reviews
{
    public int? ReviewId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    [Required(ErrorMessage = "Please select Photo")]
    public IFormFile Photo { get; set; } = null!;

    public string ImageData { get; set; } = null!;

    public DateTime PublishDate { get; set; }
}