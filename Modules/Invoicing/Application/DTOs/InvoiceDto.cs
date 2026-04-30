using InvoiceSystem.Shared;

namespace InvoiceSystem.Invoicing.Application.DTOs;

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
