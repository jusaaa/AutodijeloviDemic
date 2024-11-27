namespace AutodijeloviDemic.Models
{
    public class ShoppingCart
    {
        public int ShoppingCartId { get; set; }
        public int CustomerId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public ICollection<ShoppingCartItem> CartItems { get; set; }
    }
}
