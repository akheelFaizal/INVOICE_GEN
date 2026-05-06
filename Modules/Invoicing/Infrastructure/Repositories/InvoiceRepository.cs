using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvoiceSystem.Invoicing.Core.Entities;
using InvoiceSystem.Invoicing.Core.Interfaces;
using InvoiceSystem.Invoicing.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystem.Invoicing.Infrastructure.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly InvoicingDbContext _context;

    public InvoiceRepository(InvoicingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Invoice>> GetAllAsync()
    {
        return await _context.Invoices
            .Include(i => i.Items)
            .Include(i => i.Payments)
            .ToListAsync();
    }

    public async Task<IEnumerable<Invoice>> GetByClientIdAsync(Guid clientId)
    {
        return await _context.Invoices
            .Include(i => i.Items)
            .Include(i => i.Payments)
            .Where(i => i.ClientId == clientId)
            .ToListAsync();
    }

    public async Task<Invoice?> GetByIdAsync(Guid id)
    {
        return await _context.Invoices
            .Include(i => i.Items)
            .Include(i => i.Payments)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task AddAsync(Invoice invoice)
    {
        await _context.Invoices.AddAsync(invoice);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Invoice invoice)
    {
        _context.Invoices.Update(invoice);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Invoice invoice)
    {
        _context.Invoices.Remove(invoice);
        await _context.SaveChangesAsync();
    }

    public async Task AddItemAsync(InvoiceItem item)
    {
        await _context.InvoiceItems.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateItemAsync(InvoiceItem item)
    {
        _context.InvoiceItems.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveItemAsync(Guid itemId)
    {
        var item = await _context.InvoiceItems.FindAsync(itemId);
        if (item != null)
        {
            _context.InvoiceItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    public async Task AddPaymentAsync(Payment payment)
    {
        await _context.Payments.AddAsync(payment);
        await _context.SaveChangesAsync();
    }

    public async Task<Payment?> GetPaymentByIdAsync(Guid id)
    {
        return await _context.Payments.FindAsync(id);
    }
}
