namespace AutodijeloviDemic.Models
{
    public class EmployeeOrder
    {
        public int EmployeeOrderId { get; set; }
        public int EmployeeId { get; set; }
        public int OrderId { get; set; }

        public Employee Employee { get; set; }
        public Order Order { get; set; }
    }
}
