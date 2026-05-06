using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvoiceSystem.Invoicing.Application.DTOs;
using InvoiceSystem.Invoicing.Application.Interfaces;
using InvoiceSystem.Invoicing.Core.Entities;
using InvoiceSystem.Invoicing.Core.Interfaces;
using InvoiceSystem.Shared;

namespace InvoiceSystem.Invoicing.Application.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository;

    public InvoiceService(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task<Result<IEnumerable<InvoiceResponse>>> GetInvoicesAsync()
    {
        var invoices = await _invoiceRepository.GetAllAsync();
        var response = invoices.Select(MapToResponse);

        return Result<IEnumerable<InvoiceResponse>>.SuccessResult(response);
    }

    public async Task<Result<InvoiceResponse>> GetInvoiceByIdAsync(Guid id)
    {
        var invoice = await _invoiceRepository.GetByIdAsync(id);
        if (invoice == null) return Result<InvoiceResponse>.FailureResult("Invoice not found");

        return Result<InvoiceResponse>.SuccessResult(MapToResponse(invoice));
    }

    public async Task<Result<InvoiceResponse>> CreateInvoiceAsync(CreateInvoiceRequest request)
    {
        var invoice = new Invoice
        {
            Id = Guid.NewGuid(),
            ClientId = request.ClientId,
            Amount = request.Amount,
            Date = DateTime.UtcNow,
            DueDate = request.DueDate,
            Description = request.Description,
            Status = "Pending"
        };

        await _invoiceRepository.AddAsync(invoice);

        return Result<InvoiceResponse>.SuccessResult(MapToResponse(invoice));
    }

    public async Task<Result<InvoiceResponse>> UpdateInvoiceAsync(Guid id, UpdateInvoiceRequest request)
    {
        var invoice = await _invoiceRepository.GetByIdAsync(id);
        if (invoice == null) return Result<InvoiceResponse>.FailureResult("Invoice not found");

        invoice.Amount = request.Amount;
        invoice.DueDate = request.DueDate;
        invoice.Description = request.Description;

        await _invoiceRepository.UpdateAsync(invoice);

        return Result<InvoiceResponse>.SuccessResult(MapToResponse(invoice));
    }

    public async Task<Result> DeleteInvoiceAsync(Guid id)
    {
        var invoice = await _invoiceRepository.GetByIdAsync(id);
        if (invoice == null) return Result.FailureResult("Invoice not found");

        await _invoiceRepository.DeleteAsync(invoice);
        return Result.SuccessResult();
    }

    public async Task<Result<InvoiceItemResponse>> AddItemAsync(Guid invoiceId, InvoiceItemRequest request)
    {
        var invoice = await _invoiceRepository.GetByIdAsync(invoiceId);
        if (invoice == null) return Result<InvoiceItemResponse>.FailureResult("Invoice not found");

        var item = new InvoiceItem
        {
            Id = Guid.NewGuid(),
            InvoiceId = invoiceId,
            Description = request.Description,
            Quantity = request.Quantity,
            UnitPrice = request.UnitPrice
        };

        await _invoiceRepository.AddItemAsync(item);
        
        invoice.Amount += item.Total;
        await _invoiceRepository.UpdateAsync(invoice);

        return Result<InvoiceItemResponse>.SuccessResult(MapToItemResponse(item));
    }

    public async Task<Result<InvoiceItemResponse>> UpdateItemAsync(Guid invoiceId, Guid itemId, InvoiceItemRequest request)
    {
        var invoice = await _invoiceRepository.GetByIdAsync(invoiceId);
        if (invoice == null) return Result<InvoiceItemResponse>.FailureResult("Invoice not found");

        var item = invoice.Items.FirstOrDefault(i => i.Id == itemId);
        if (item == null) return Result<InvoiceItemResponse>.FailureResult("Item not found");

        invoice.Amount -= item.Total;
        
        item.Description = request.Description;
        item.Quantity = request.Quantity;
        item.UnitPrice = request.UnitPrice;

        invoice.Amount += item.Total;

        await _invoiceRepository.UpdateItemAsync(item);
        await _invoiceRepository.UpdateAsync(invoice);

        return Result<InvoiceItemResponse>.SuccessResult(MapToItemResponse(item));
    }

    public async Task<Result> RemoveItemAsync(Guid invoiceId, Guid itemId)
    {
        var invoice = await _invoiceRepository.GetByIdAsync(invoiceId);
        if (invoice == null) return Result.FailureResult("Invoice not found");

        var item = invoice.Items.FirstOrDefault(i => i.Id == itemId);
        if (item == null) return Result.FailureResult("Item not found");

        invoice.Amount -= item.Total;
        
        await _invoiceRepository.RemoveItemAsync(itemId);
        await _invoiceRepository.UpdateAsync(invoice);

        return Result.SuccessResult();
    }

    public async Task<Result> UpdateStatusAsync(Guid id, UpdateStatusRequest request)
    {
        var invoice = await _invoiceRepository.GetByIdAsync(id);
        if (invoice == null) return Result.FailureResult("Invoice not found");

        invoice.Status = request.Status;
        await _invoiceRepository.UpdateAsync(invoice);

        return Result.SuccessResult();
    }

    public async Task<Result<IEnumerable<PaymentResponse>>> GetPaymentsAsync(Guid invoiceId)
    {
        var invoice = await _invoiceRepository.GetByIdAsync(invoiceId);
        if (invoice == null) return Result<IEnumerable<PaymentResponse>>.FailureResult("Invoice not found");

        return Result<IEnumerable<PaymentResponse>>.SuccessResult(invoice.Payments.Select(MapToPaymentResponse));
    }

    public async Task<Result<PaymentResponse>> AddPaymentAsync(Guid invoiceId, PaymentRequest request)
    {
        var invoice = await _invoiceRepository.GetByIdAsync(invoiceId);
        if (invoice == null) return Result<PaymentResponse>.FailureResult("Invoice not found");

        var payment = new Payment
        {
            Id = Guid.NewGuid(),
            InvoiceId = invoiceId,
            Amount = request.Amount,
            Date = DateTime.UtcNow,
            PaymentMethod = request.PaymentMethod
        };

        await _invoiceRepository.AddPaymentAsync(payment);

        return Result<PaymentResponse>.SuccessResult(MapToPaymentResponse(payment));
    }

    public async Task<Result<PaymentResponse>> GetPaymentByIdAsync(Guid id)
    {
        var payment = await _invoiceRepository.GetPaymentByIdAsync(id);
        if (payment == null) return Result<PaymentResponse>.FailureResult("Payment not found");

        return Result<PaymentResponse>.SuccessResult(MapToPaymentResponse(payment));
    }

    public async Task<Result<InvoiceBalanceResponse>> GetInvoiceBalanceAsync(Guid id)
    {
        var invoice = await _invoiceRepository.GetByIdAsync(id);
        if (invoice == null) return Result<InvoiceBalanceResponse>.FailureResult("Invoice not found");

        var totalPaid = invoice.Payments.Sum(p => p.Amount);
        var balance = invoice.Amount - totalPaid;

        return Result<InvoiceBalanceResponse>.SuccessResult(new InvoiceBalanceResponse(invoice.Amount, totalPaid, balance));
    }

    public async Task<Result<DashboardSummaryResponse>> GetDashboardSummaryAsync()
    {
        var invoices = await _invoiceRepository.GetAllAsync();
        
        var totalInvoices = invoices.Count();
        var totalBilled = invoices.Sum(i => i.Amount);
        var totalPaid = invoices.Sum(i => i.Payments.Sum(p => p.Amount));
        var totalOutstanding = totalBilled - totalPaid;
        var pendingInvoices = invoices.Count(i => i.Status == "Pending");
        var overdueInvoices = invoices.Count(i => i.Status != "Paid" && i.DueDate < DateTime.UtcNow);

        return Result<DashboardSummaryResponse>.SuccessResult(new DashboardSummaryResponse(
            totalInvoices,
            totalBilled,
            totalPaid,
            totalOutstanding,
            pendingInvoices,
            overdueInvoices
        ));
    }

    private static InvoiceResponse MapToResponse(Invoice invoice)
    {
        return new InvoiceResponse(
            invoice.Id,
            invoice.ClientId,
            invoice.Amount,
            invoice.Date,
            invoice.DueDate,
            invoice.Description,
            invoice.Status,
            invoice.Items.Select(MapToItemResponse),
            invoice.Payments.Select(MapToPaymentResponse)
        );
    }

    private static InvoiceItemResponse MapToItemResponse(InvoiceItem item)
    {
        return new InvoiceItemResponse(item.Id, item.Description, item.Quantity, item.UnitPrice, item.Total);
    }

    private static PaymentResponse MapToPaymentResponse(Payment payment)
    {
        return new PaymentResponse(payment.Id, payment.Amount, payment.Date, payment.PaymentMethod);
    }
}
