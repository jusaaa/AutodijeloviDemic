using System.ComponentModel.DataAnnotations;

namespace AutodijeloviDemic.Models
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }

        [Range(1, int.MaxValue,ErrorMessage = "Količina mora biti veća od 0.")]
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;

        public Product Product { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
