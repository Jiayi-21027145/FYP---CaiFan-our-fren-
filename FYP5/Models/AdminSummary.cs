using System.Collections.Generic;

namespace FYP5.Models
{
    public class AdminSummary
    {
        public class ReviewRatingCount
        {
            public int Rating { get; set; }
            public int Count { get; set; }
        }

        public class UploadsPerYear
        {
            public int TotalUploads { get; set; }

        }

        public class LocationDishCount
        {
            public List<string>? TopDishes { get; set; }
            public List<int>? TopDishesCount { get; set; }
        }

        public List<ReviewRatingCount> ReviewRatingCounts { get; set; } = new List<ReviewRatingCount>();
        public List<LocationDishCount> TopDishesByLocation { get; set; } = new List<LocationDishCount>();
        public int TotalImageUploads { get; set; }
    }
}