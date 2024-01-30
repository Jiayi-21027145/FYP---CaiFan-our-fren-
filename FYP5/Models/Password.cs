using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FYP5.Models
{
    public class  Password
    {
        [Required(ErrorMessage = "UserId required")]
        [Remote(action: "ForgotPassword", controller: "Account")]
        public string UserId { get; set; } = null!;

        [Required(ErrorMessage = "New Password Required")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Invalid Password Format. Password must have minimum 8 characters, 1 uppercase, 1 lowercase, 1 digit and 1 special character.")]
        [DataType(DataType.Password)]
        [Remote("VerifyNewPassword", "Account",
          ErrorMessage = "New Password Invalid")]
        public string NewPassword { get; set; } = null!;

        [Required(ErrorMessage = "Confirm Password Required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords Unmatched")]
        public string ConfirmPassword { get; set; } = null!;
    }
}

