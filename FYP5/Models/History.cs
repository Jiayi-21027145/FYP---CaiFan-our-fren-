namespace FYP5.Models
{
    public class History
    {
        public string UserId { get; set; } = null!;

        public int Id { get; set; }

        public string DishOne { get; set; } = null!;

        public string DishTwo { get; set; } = null!;
        public string DishThree { get; set; } = null!;
        public string DishFour { get; set; } = null!;
        public string DishFive { get; set; } = null!;
        public string DishSix { get; set; } = null!;

        public string CaloriesRange { get; set; } = null!;
        public string AveragePrice { get; set; } = null!;
        public string Image { get; set; } = null!;

        public virtual JiakUser User { get; set; } = null!;
    }
}
