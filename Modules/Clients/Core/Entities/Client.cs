namespace InvoiceSystem.Clients.Core.Entities;

public class Client
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public decimal OutstandingBalance { get; set; }
    public decimal TotalBilled { get; set; }
    public Guid? AssignedStaffId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
