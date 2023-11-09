using System;
using System.Collections.Generic;

namespace FYP3.Models;

public partial class UserRoles
{
    public int Id { get; set; }

    public string? UserId { get; set; }

    public int? RoleId { get; set; }

    public virtual UserRole? Role { get; set; }

    public virtual Users? User { get; set; }
}
