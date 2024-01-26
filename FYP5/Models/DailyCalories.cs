namespace FYP5.Models
{
    public class DailyCalories
    {
        public int Id { get; set; }
        public int MinimumCalories { get; set; }
        public int MaximumCalories { get; set; }
        public int AverageCalories { get; set; }
        public string Gender { get; set; } = null!;
    }

}
