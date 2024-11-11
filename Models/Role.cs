namespace AutodijeloviDemic.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } // npr. "Admin", "Product Manager", "Order Manager"
        public ICollection<Employee> Employees { get; set; }
    }
}
