using System;
using System.Collections.Generic;

namespace FYP3.Models;

public partial class Users
{
    public string UserID { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string Reference { get; set; } = null!;

    public virtual ICollection<ImageUploads> ImageUploads { get; set; } = new List<ImageUploads>();

    public virtual ICollection<Reviews> Reviews { get; set; } = new List<Reviews>();

    public virtual ICollection<UserHistory> UserHistory { get; set; } = new List<UserHistory>();

    public virtual ICollection<UserRoles> UserRoles { get; set; } = new List<UserRoles>();
}
