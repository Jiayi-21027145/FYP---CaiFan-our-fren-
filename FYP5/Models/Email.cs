using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FYP5.Models;

public class Email
{
    [Required(ErrorMessage = "UserId is required.")]
    public string UserId { get; set; } = null!;

    [Required(ErrorMessage = "UserId is required.")]
    public string UserPw { get; set; } = null!;

    [Required(ErrorMessage = "User email address is required.")]
    [EmailAddress(ErrorMessage = "Email address is not valid.")]
    public string UserEmail { get; set; } = null!;


    //[Remote("VerifyEmail", "Users")]  The Users class is not included.
    public string Vmail { get; set; } = null!;
}

