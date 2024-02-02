using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FYP5.Models;

public partial class JiakUser
{
    
    [Required(ErrorMessage = "Please enter a user Id.")]
    [Remote(action: "VerifyUserID", controller: "Account")]
    [RegularExpression(@"^[a-zA-Z0-9]{3,10}$", ErrorMessage = "Invalid UserId Format. UserId only contain letters and digits.")]
    public string UserId { get; set; } = null!;

    [Required(ErrorMessage = "Please enter Password")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Invalid Password Format. Password must have minimum 8 characters, 1 uppercase, 1 lowercase, 1 digit and 1 special character.")]
    public string UserPw { get; set; } = null!;
    
    [NotMapped]
    [Compare("UserPw", ErrorMessage = "Passwords do not match.")]
    [DataType(DataType.Password)]
    public string UserPw2 { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a username.")]
    [RegularExpression(@"^[A-Za-z][A-Za-z0-9_]{3,29}$", ErrorMessage = "Invalid UserName Format. UserName does not contain special characters.")]
    public string UserName { get; set; } = null!;

    [Required]
    public string Gender { get; set; } = null!;
    [Required(ErrorMessage = "Please enter an email address.")]
    [EmailAddress(ErrorMessage = "Email address is not valid.")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$", ErrorMessage = "Invalid Email Format.")]

    public string Email { get; set; } = null!;
    
    public string UserRole { get; set; } = null!;

    public DateTime? LastLogin { get; set; }

    public virtual ICollection<Dataset> Dataset { get; set; } = new List<Dataset>();

    public virtual ICollection<History> History { get; set; } = new List<History>();
}