namespace Online_Shoes_Shop.Models
{
    public class ShoesModel
    {
        public int ShoeId { get; set; } // Corresponds to ShoeId (Primary Key)
        public string Name { get; set; } // Corresponds to Name
        public int CategoryId { get; set; } // Corresponds to CategoryId (Foreign Key referencing Categories table)
        public string CategoryName { get; set; }

        public double Price { get; set; } // Corresponds to Price
        public string ImageURL { get; set; } // Corresponds to ImageURL
        public byte[] Image { get; set; } // Corresponds to Image (stored as binary data)
        public string Description { get; set; } // Corresponds to Description
        public int Stock { get; set; } // Corresponds to Stock
        public DateTime CreatedDate { get; set; } // Corresponds to CreatedDate
        public DateTime ModifiedDate { get; set; } // Corresponds to ModifiedDate

        // Navigation property for the related Category
        //public CategoriesModel Category { get; set; }
    }
}
