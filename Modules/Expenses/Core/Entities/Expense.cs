using System;

namespace InvoiceSystem.Expenses.Core.Entities;

public class Expense
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public Guid CategoryId { get; set; }
    public string? ReceiptFileId { get; set; }
    
    // Navigation property
    public ExpenseCategory Category { get; set; } = null!;
}

public class ExpenseCategory
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
}
