using System;
using System.Collections.Generic;

namespace FYP2.Models;

public partial class UserRoles
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? RoleId { get; set; }

    public virtual UserRole? Role { get; set; }

    public virtual Users? User { get; set; }
}
