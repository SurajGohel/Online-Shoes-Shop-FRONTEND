using System.ComponentModel.DataAnnotations;

namespace Online_Shoes_Shop.Models
{
    public class OrderModel
    {
        public int? OrderId { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [MinLength(12, ErrorMessage = "Address must be at least 12 characters long")]
        [MaxLength(100, ErrorMessage = "Address cannot exceed 100 characters")]
        [RegularExpression(@"^(?!\d+$)[a-zA-Z0-9\s,.-]+$", ErrorMessage = "Address cannot be only digits")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        [MinLength(4, ErrorMessage = "City must be at least 4 characters long")]
        [MaxLength(50, ErrorMessage = "City cannot exceed 50 characters")]
        [RegularExpression(@"^(?=.*[a-zA-Z])[\w\s]+$", ErrorMessage = "City must contain at least one letter and cannot be only digits")]
        public string City { get; set; }


        [Required(ErrorMessage = "State is required")]
        [MinLength(4, ErrorMessage = "State must be at least 4 characters long")]
        [MaxLength(50, ErrorMessage = "State cannot exceed 50 characters")]
        [RegularExpression(@"^(?=.*[a-zA-Z])[\w\s]+$", ErrorMessage = "State must contain at least one letter and cannot be only digits")]
        public string State { get; set; }

        [Required(ErrorMessage = "Pincode is required")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Pincode must be exactly 6 digits")]
        public string Pincode { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be exactly 10 digits")]
        public string MobileNumber { get; set; }
    }
}
