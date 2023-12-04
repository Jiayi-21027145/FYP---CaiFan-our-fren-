using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FYP5.Models
{
    public class Password
    {
        [Required(ErrorMessage = "New Password Required")]
        [DataType(DataType.Password)]
        [Remote("VerifyNewPassword", "Account",
          ErrorMessage = "New Password Invalid")]
        public string NewPassword { get; set; } = null!;

        [Required(ErrorMessage = "Confirm Password Required")]
        [DataType(DataType.Password)]
        [Compare("NewPwd", ErrorMessage = "Passwords Unmatched")]
        public string ConfirmPassword { get; set; } = null!;
    }
}

