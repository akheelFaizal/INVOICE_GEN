using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvoiceSystem.Reporting.Application.Interfaces;
using InvoiceSystem.Invoicing.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystem.Invoicing.Infrastructure.Services;

public class InvoiceDataService : IInvoiceDataService
{
    private readonly IInvoiceRepository _repository;

    public InvoiceDataService(IInvoiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<decimal> GetTotalRevenueAsync(DateTime startDate, DateTime endDate)
    {
        var invoices = await _repository.GetAllAsync();
        return invoices
            .Where(i => i.Date >= startDate && i.Date <= endDate && i.Status == "Paid")
            .Sum(i => i.Amount);
    }

    public async Task<IEnumerable<object>> GetTopClientsAsync(int count)
    {
        var invoices = await _repository.GetAllAsync();
        return invoices
            .GroupBy(i => i.ClientId)
            .Select(g => new { ClientId = g.Key, TotalBilled = g.Sum(i => i.Amount) })
            .OrderByDescending(x => x.TotalBilled)
            .Take(count)
            .ToList<object>();
    }

    public async Task<IEnumerable<object>> GetInvoiceReportAsync(DateTime? start, DateTime? end, Guid? clientId, string? status)
    {
        var invoices = await _repository.GetAllAsync();
        var query = invoices.AsQueryable();
        
        if (start.HasValue) query = query.Where(i => i.Date >= start.Value);
        if (end.HasValue) query = query.Where(i => i.Date <= end.Value);
        if (clientId.HasValue) query = query.Where(i => i.ClientId == clientId.Value);
        if (!string.IsNullOrEmpty(status)) query = query.Where(i => i.Status == status);

        return query.Select(i => new { i.Id, i.ClientId, i.Amount, i.Date, i.Status }).ToList<object>();
    }
}
