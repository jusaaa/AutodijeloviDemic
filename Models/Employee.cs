namespace AutodijeloviDemic.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; } // npr. "Admin", "Manager", "Sales"
        public DateTime HireDate { get; set; }

        // Navigaciona svojstva
        public ICollection<Product> ManagedProducts { get; set; }  // Proizvodi kojima zaposleni upravlja
        public ICollection<Order> ManagedOrders { get; set; }      // Narudžbe kojima zaposleni upravlja
    }
}
