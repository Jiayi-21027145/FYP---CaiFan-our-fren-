using System.ComponentModel.DataAnnotations;

namespace FYP5.Models;

public class UserLogin
{
    [Required(ErrorMessage = "Please enter User ID")]
    public string UserID { get; set; } = null!;

    [Required(ErrorMessage = "Please enter Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    // TODO: Lesson11 Task 1b - Create a boolean property named RememberMe
    public bool RememberMe { get; set; }
    

}
