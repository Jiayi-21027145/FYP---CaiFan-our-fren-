using System;
using System.Collections.Generic;

namespace FYP3.Models;

public partial class Reviews
{
    public int ReviewId { get; set; }

    public string? UserId { get; set; }

    public int? ImageId { get; set; }

    public int Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? ReviewDate { get; set; }

    public virtual Dataset? Image { get; set; }

    public virtual Users? User { get; set; }
}
