//namespace FYP5.Models
//{
//    public class Summary
//    {
//        public string? UserId { get; set; }
//        public int TotalCalories { get; set; }
//        public int TotalCount { get; set; }
//        public int TotalPic{ get; set; }
//        public int Price { get; set; }
//        public int PriceTotal { get; set;}
//        public object Items { get; internal set; }
//    }
//}

namespace FYP5.Models
{
    public class Summary
    {
        public string? UserId { get; set; } // Assuming UserId is a string, adjust the type as necessary
    
        public string Location { get; set; } = null!;
        public string LocationName { get; set; } = null!;// Assuming a single Location entity
        public string Item { get; set; } = null!;
        public int ItemID { get; set; }
      
    }
}


