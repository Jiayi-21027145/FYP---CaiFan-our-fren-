using System;
using System.Collections.Generic;

namespace FYP6.Models;

public partial class JiakUser2
{
    public string UserId { get; set; } = null!;

    public byte[] UserPw { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string UserRole { get; set; } = null!;

    public DateTime? LastLogin { get; set; }

    public virtual ICollection<ImageUploads> ImageUploads { get; set; } = new List<ImageUploads>();

    public virtual ICollection<UserHistory> UserHistory { get; set; } = new List<UserHistory>();
}
