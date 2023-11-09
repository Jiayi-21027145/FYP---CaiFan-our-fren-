using System;
using System.Collections.Generic;

namespace FYP3.Models;

public partial class UserHistory
{
    public int HistoryId { get; set; }

    public string? UserId { get; set; }

    public string Action { get; set; } = null!;

    public DateTime ActionDate { get; set; }

    public string? Description { get; set; }

    public virtual Users? User { get; set; }
}
