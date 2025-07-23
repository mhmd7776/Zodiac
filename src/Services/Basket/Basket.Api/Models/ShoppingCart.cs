namespace Basket.Api.Models
{
    public class ShoppingCart
    {
        public ShoppingCart(string userName)
        {
            UserName = userName;
        }

        // Required for mapping
        public ShoppingCart() { }

        public string? UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = [];
        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
    }
}
