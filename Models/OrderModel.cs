using System.ComponentModel.DataAnnotations;

namespace Online_Shoes_Shop.Models
{
    public class OrderModel
    {
        public int? OrderId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]

        public string City { get; set; }

        [Required]

        public string State { get; set; }

        [Required]
        public string Pincode { get; set; }

        [Required]
        public string MobileNumber { get; set; }

    }
}
