
using System.ComponentModel.DataAnnotations;

namespace FYP5.Models;

public class Reviews
{
    public int ReviewId { get; set; }

    public int Rating { get; set; }

    [Required(ErrorMessage = "Please enter Title")]
    [StringLength(100, ErrorMessage = "Max 100 chars")]
    public string? Comment { get; set; }

    [Required(ErrorMessage = "Please select Photo")]
    public IFormFile Photo { get; set; } = null!;

    public string ImageData { get; set; } = null!;
}
