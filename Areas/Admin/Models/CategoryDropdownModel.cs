using System.ComponentModel.DataAnnotations;

    public class CategoryDropdownModel
    {
        public int CategoryId { get; set; } // Corresponds to CategoryId (Primary Key)
        [Required]
        public string CategoryName { get; set; } // Corresponds to CategoryName
    }
