using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InvoiceSystem.Invoicing.Core.Entities;

namespace InvoiceSystem.Invoicing.Core.Interfaces;

public interface IInvoiceRepository
{
    Task<IEnumerable<Invoice>> GetAllAsync();
    Task<IEnumerable<Invoice>> GetByClientIdAsync(Guid clientId);
    Task<Invoice?> GetByIdAsync(Guid id);
    Task AddAsync(Invoice invoice);
    Task UpdateAsync(Invoice invoice);
    Task DeleteAsync(Invoice invoice);
    
    Task AddItemAsync(InvoiceItem item);
    Task UpdateItemAsync(InvoiceItem item);
    Task RemoveItemAsync(Guid itemId);
    Task AddPaymentAsync(Payment payment);
}
