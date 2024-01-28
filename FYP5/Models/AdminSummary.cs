using System.Collections.Generic;

namespace FYP5.Models
{
    public class AdminSummaryViewModel
    {
        public int TotalUsers { get; set; }
        public int UsersWithUploads { get; set; }
        public List<ImageUpload> UploadedDishes { get; set; } = new List<ImageUpload>();
    }

    public class ImageUpload
    {
        // ... other properties ...

        public int UserID { get; set; }
        public virtual JiakUser User { get; set; } = new JiakUser(); // Provide a default instance
        //public virtual ICollection<Dish> Dishes { get; set; } = new List<Dish>(); // Provide a default collection
        //public string ImageData { get; set; }
    }

    public class MostLeastOrderedDishViewModel
    {
        public DishViewModel MostOrderedDish { get; set; } = new DishViewModel();
        public DishViewModel LeastOrderedDish { get; set; } = new DishViewModel();
    }

    public class ReviewRatingChartViewModel
    {
        public List<ReviewRatingCount> RatingCounts { get; set; } = new List<ReviewRatingCount>();
    }

    public class DishViewModel
    {
        public string DishName { get; set; } = string.Empty; // Provide an empty string as a default
        public int OrderCount { get; set; }
    }

    public class ReviewRatingCount
    {
        public int Rating { get; set; }
        public int Count { get; set; }
    }


    public class UserPictureUploadViewModel
    {
        public int UserId { get; set; }
        public required string UserName { get; set; }
        public int TotalUploads { get; set; }
    }

}