namespace AutodijeloviDemic.Models
{
    public class EmployeeProduct
   {
        public int EmployeeProductId { get; set; }
        public int EmployeeId { get; set; }
        public int ProductId { get; set; }

        public Employee Employee { get; set; }
        public Product Product { get; set; }
    }
}
