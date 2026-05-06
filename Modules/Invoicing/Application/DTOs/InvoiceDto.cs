using System;
using System.Collections.Generic;
using InvoiceSystem.Shared;

namespace InvoiceSystem.Invoicing.Application.DTOs;

public record CreateInvoiceRequest(Guid ClientId, decimal Amount, DateTime DueDate, string Description);

public record UpdateInvoiceRequest(decimal Amount, DateTime DueDate, string Description);

public record InvoiceResponse(
    Guid Id, 
    Guid ClientId, 
    decimal Amount, 
    DateTime Date, 
    DateTime DueDate, 
    string Description, 
    string Status,
    IEnumerable<InvoiceItemResponse> Items,
    IEnumerable<PaymentResponse> Payments);

public record InvoiceItemRequest(string Description, int Quantity, decimal UnitPrice);

public record InvoiceItemResponse(Guid Id, string Description, int Quantity, decimal UnitPrice, decimal Total);

public record PaymentRequest(decimal Amount, string PaymentMethod);

public record PaymentResponse(Guid Id, decimal Amount, DateTime Date, string PaymentMethod);

public record UpdateStatusRequest(string Status);

public record InvoiceBalanceResponse(decimal TotalAmount, decimal TotalPaid, decimal Balance);

public record DashboardSummaryResponse(
    int TotalInvoices,
    decimal TotalBilled,
    decimal TotalPaid,
    decimal TotalOutstanding,
    int PendingInvoices,
    int OverdueInvoices);
