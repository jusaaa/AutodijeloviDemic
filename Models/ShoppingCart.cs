namespace AutodijeloviDemic.Models
{
    public class ShoppingCart
    {
        public int ShoppingCartId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<ShoppingCartItem> CartItems { get; set; }
    }
}
