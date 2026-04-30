using InvoiceSystem.Invoicing.Application.DTOs;
using InvoiceSystem.Shared;

namespace InvoiceSystem.Invoicing.Application.Interfaces;

public interface IInvoiceService
{
    Task<Result<InvoiceResponse>> CreateInvoiceAsync(CreateInvoiceRequest request);
    Task<Result<IEnumerable<InvoiceResponse>>> GetInvoicesAsync();
}
