namespace InvoiceSystem.Invoicing.Core.DTOs;

public record CreateInvoiceRequest(
    string ClientName,
    decimal TotalAmount,
    DateTime DueDate
);

public record InvoiceResponse(
    Guid Id,
    string ClientName,
    decimal TotalAmount,
    DateTime DueDate,
    string Status
);
