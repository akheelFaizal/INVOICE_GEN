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

        var response = MapToResponse(invoice);
        return Result<InvoiceResponse>.SuccessResult(response);
    }

    public async Task<Result<IEnumerable<InvoiceResponse>>> GetInvoicesAsync()
    {
        var invoices = await _invoiceRepository.GetAllAsync();
        var response = invoices.Select(MapToResponse);

        return Result<IEnumerable<InvoiceResponse>>.SuccessResult(response);
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
            invoice.Status
        );
    }
}
