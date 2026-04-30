namespace InvoiceSystem.Clients.Core.Interfaces;

public interface IInvoiceIntegrationService
{
    Task<IEnumerable<object>> GetInvoicesByClientIdAsync(Guid clientId);
}
