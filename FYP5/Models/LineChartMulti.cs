using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP5.Models
{
    public class Chart
    {
        public string[] labels { get; set; } = null!;
        public List<Datasets> Datasets { get; set; } = null!;
        public int? SelectedYear { get; set; }
        public SelectList Years { get; set; } = null!;
        public List<string> Months { get; set; } = null!;
        public List<int> AverageCalories { get; set; } = null!;

        public string UserId { get; set; } = null!;
    }
    public class Datasets
    {
        public string label { get; set; } = null!;
        public string[] backgroundColor { get; set; } = null!;
        public string[] borderColor { get; set; } = null!;
        public string borderWidth { get; set; } = null!;
        public int[] data { get; set; } = null!;
    }
}