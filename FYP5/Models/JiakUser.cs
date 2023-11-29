using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FYP5.Models;

public partial class JiakUser
{
    [Required(ErrorMessage = "Please enter a user Id.")]
    public string UserId { get; set; } = null!;
    [Required(ErrorMessage = "Please enter Password")]
    [DataType(DataType.Password)]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "Password must at least 5 characters.")]
    public string UserPw { get; set; } = null!;
    [Compare("UserPw", ErrorMessage = "Passwords do not match.")]
    [DataType(DataType.Password)]
    public string UserPw2 { get; set; } = null!;
    [Required(ErrorMessage = "Please enter a full name.")]
    public string UserName { get; set; } = null!;
    [Required(ErrorMessage = "Please enter an email address.")]
    [EmailAddress(ErrorMessage = "Email address is not valid.")]
    public string Email { get; set; } = null!;

    public string UserRole { get; set; } = null!;

    public DateTime? LastLogin { get; set; }

    public virtual ICollection<ImageUploads> ImageUploads { get; set; } = new List<ImageUploads>();
}
