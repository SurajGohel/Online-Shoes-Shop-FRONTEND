namespace Online_Shoes_Shop.Models
{
    public class CartModel
    {
            public int? CartItemId { get; set; }
            public string UserId { get; set; }
            public int ShoeId { get; set; }
            public int Quantity { get; set; }

            // Shoe-related properties
            public string ShoeName { get; set; }
            public decimal ShoePrice { get; set; }
            public string ShoeImageURL { get; set; }
    }

    public class AddCartModel
    {
        public int? CartItemId { get; set; }
        public string UserId { get; set; }
        public int ShoeId { get; set; }
        public int Quantity { get; set; }
    }
}
