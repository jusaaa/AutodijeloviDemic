namespace AutodijeloviDemic.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public decimal TotalAmount { get; set; }
        // Status narudžbe (npr. Pending, Shipped, Delivered, Canceled)
        public string Status { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
