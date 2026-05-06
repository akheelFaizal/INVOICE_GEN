using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InvoiceSystem.Invoicing.Application.DTOs;
using InvoiceSystem.Shared;

namespace InvoiceSystem.Invoicing.Application.Interfaces;

public interface IInvoiceService
{
    Task<Result<IEnumerable<InvoiceResponse>>> GetInvoicesAsync();
    Task<Result<InvoiceResponse>> GetInvoiceByIdAsync(Guid id);
    Task<Result<InvoiceResponse>> CreateInvoiceAsync(CreateInvoiceRequest request);
    Task<Result<InvoiceResponse>> UpdateInvoiceAsync(Guid id, UpdateInvoiceRequest request);
    Task<Result> DeleteInvoiceAsync(Guid id);

    Task<Result<InvoiceItemResponse>> AddItemAsync(Guid invoiceId, InvoiceItemRequest request);
    Task<Result<InvoiceItemResponse>> UpdateItemAsync(Guid invoiceId, Guid itemId, InvoiceItemRequest request);
    Task<Result> RemoveItemAsync(Guid invoiceId, Guid itemId);

    Task<Result> UpdateStatusAsync(Guid id, UpdateStatusRequest request);

    Task<Result<IEnumerable<PaymentResponse>>> GetPaymentsAsync(Guid invoiceId);
    Task<Result<PaymentResponse>> AddPaymentAsync(Guid invoiceId, PaymentRequest request);
    Task<Result<PaymentResponse>> GetPaymentByIdAsync(Guid id);

    Task<Result<InvoiceBalanceResponse>> GetInvoiceBalanceAsync(Guid id);
    Task<Result<DashboardSummaryResponse>> GetDashboardSummaryAsync();
}
