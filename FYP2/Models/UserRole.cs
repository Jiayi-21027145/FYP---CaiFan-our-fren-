using System;
using System.Collections.Generic;

namespace FYP2.Models;

public partial class UserRole
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<UserRoles> UserRoles { get; set; } = new List<UserRoles>();
}
