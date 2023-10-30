using System;
using System.Collections.Generic;

namespace FYP.Models;

public partial class Users
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public byte[] Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool IsAdmin { get; set; }

    public virtual ICollection<ImageUploads> ImageUploads { get; set; } = new List<ImageUploads>();

    public virtual ICollection<Reviews> Reviews { get; set; } = new List<Reviews>();
}
