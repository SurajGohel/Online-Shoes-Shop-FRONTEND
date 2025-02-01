namespace Online_Shoes_Shop.Models
{
    public class GetUserOrdersModel
    {
        public int OrderId {  get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalAmount {  get; set; }
        public string PaymentMethod { get; set;}
        public string Status { get; set; }
        public DateTime LastStatusChangedDate { get; set; }


    }
}
