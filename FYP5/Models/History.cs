using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FYP5.Models
{
    public class History
    {
        public string UserId { get; set; } = null!;

        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime UploadDate { get; set; }
        public string Location { get; set; } = null!;
        public string DishOne { get; set; } = null!;

        public string DishTwo { get; set; } = null!;
        public string DishThree { get; set; } = null!;
        public string DishFour { get; set; } = null!;
        public string DishFive { get; set; } = null!;
        public string DishSix { get; set; } = null!;

        public string CaloriesRange { get; set; } = null!;

        public int AverageCalories { get; set; }
        public string PriceRange { get; set; } = null!;

        public decimal AveragePrice {get; set;}
        public string Image { get; set; } = null!;

        public virtual JiakUser User { get; set; } = null!;
    }



}

