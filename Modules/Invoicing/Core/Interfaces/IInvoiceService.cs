using InvoiceSystem.Invoicing.Core.DTOs;

namespace InvoiceSystem.Invoicing.Core.Interfaces;

public interface IInvoiceService
{
    Task<InvoiceResponse> CreateInvoiceAsync(CreateInvoiceRequest request);
    Task<IEnumerable<InvoiceResponse>> GetInvoicesAsync();
}
