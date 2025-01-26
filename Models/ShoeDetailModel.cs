namespace Online_Shoes_Shop.Models
{
    public class ShoeDetailModel
    {
        // Shoes Details
        public int ShoeId { get; set; } // Corresponds to ShoeId (Primary Key)
        public string Name { get; set; } // Corresponds to Name
        public int CategoryId { get; set; } // Corresponds to CategoryId (Foreign Key referencing Categories table)
        public string CategoryName { get; set; } // Corresponds to CategoryName from Categories table
        public double Price { get; set; } // Corresponds to Price
        public string ImageURL { get; set; } // Corresponds to ImageURL
        //public byte[] Image { get; set; } // Corresponds to Image (stored as binary data)
        public string Description { get; set; } // Corresponds to Description
        public int Stock { get; set; } // Corresponds to Stock

        // Aggregated Review Details
        public double? AverageRating { get; set; } // Average Rating from Reviews
        public int TotalReviews { get; set; } // Total number of Reviews

        // Individual Review Details
        public string UserName { get; set; } // User who wrote the review
        public string Comment { get; set; } // Review Comment
        public DateTime? ReviewDate { get; set; } // Review Date
        public int? Rating { get; set; } // Review Rating (1 to 5)
    }
}
