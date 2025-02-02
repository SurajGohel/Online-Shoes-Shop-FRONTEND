namespace Online_Shoes_Shop.Models
{
    public class GetUserOrdersModel
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public int OrderId {  get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalAmount {  get; set; }
        public string PaymentMethod { get; set;}
        public string Status { get; set; }
        public DateTime LastStatusChangedDate { get; set; }
    }

    public class GetOrderByOrderIdModel
    {
        public string Status { get; set; }
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public string PaymentMethod { get; set; }
        public string MobileNumber { get; set; }
        public double TotalAmount { get; set; }
    }

    public class GetOrderDetailByOrderIdModel
    {
        public int ShoeId { get; set; }
        public string ImageURL { get; set; }
        public string ShoeName { get; set; }
        public int Quantity { get; set; }
        public string CategoryName { get; set; }
        public double Price { get; set; }
    }
}
