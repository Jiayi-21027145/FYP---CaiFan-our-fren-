using System;
using System.Collections.Generic;

namespace FYP4.Models;

public partial class UserHistory
{
    public int Id { get; set; }

    public decimal Price { get; set; }

    public int? NutritionalValue { get; set; }

    public string? Description { get; set; }

    public byte[] ImageData { get; set; } = null!;
}
