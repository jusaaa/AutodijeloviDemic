namespace AutodijeloviDemic.Models
{
    public class Shipping
    {
        public int ShippingId { get; set; }
        public int OrderId { get; set; }
        public string ShippingMethod { get; set; } // npr. "Standard", "Express"
        public DateTime ShippingDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string Status { get; set; } // npr. "In Transit", "Delivered", "Returned"

        public Order Order { get; set; }
    }
}
