using System;
using System.Collections.Generic;

namespace FYP5.Models;

public partial class JiakUser
{
    public string UserId { get; set; } = null!;

    public byte[] UserPw { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string UserRole { get; set; } = null!;

    public DateTime? LastLogin { get; set; }

    public virtual ICollection<ImageUploads> ImageUploads { get; set; } = new List<ImageUploads>();
}
