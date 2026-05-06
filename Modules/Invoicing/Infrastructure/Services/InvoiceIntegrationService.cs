using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvoiceSystem.Clients.Core.Interfaces;
using InvoiceSystem.Invoicing.Core.Interfaces;

namespace InvoiceSystem.Invoicing.Infrastructure.Services;

public class InvoiceIntegrationService : IInvoiceIntegrationService
{
    private readonly IInvoiceRepository _invoiceRepository;

    public InvoiceIntegrationService(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task<IEnumerable<object>> GetInvoicesByClientIdAsync(Guid clientId)
    {
        var invoices = await _invoiceRepository.GetByClientIdAsync(clientId);
        
        // Return anonymous objects or map to a generic dictionary/DTO as required by the object return type.
        return invoices.Select(i => new 
        {
            i.Id,
            i.Amount,
            i.Date,
            i.DueDate,
            i.Description,
            i.Status
        }).ToList<object>();
    }
}
