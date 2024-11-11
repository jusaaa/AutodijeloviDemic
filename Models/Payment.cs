namespace AutodijeloviDemic.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } // npr. "Credit Card", "PayPal", "Bank Transfer"
        public string Status { get; set; } // npr. "Completed", "Pending", "Failed"

        public Order Order { get; set; }
    }
}
