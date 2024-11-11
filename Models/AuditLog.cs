namespace AutodijeloviDemic.Models
{
    public class AuditLog
    {
        public int AuditLogId { get; set; }
        public int EmployeeId { get; set; }
        public string Action { get; set; }      // npr. "Created Product", "Updated Price", "Changed Order Status"
        public DateTime ActionDate { get; set; }

        public Employee Employee { get; set; }
    }
}
