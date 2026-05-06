using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvoiceSystem.Reporting.Application.Interfaces;
using InvoiceSystem.Expenses.Core.Interfaces;

namespace InvoiceSystem.Expenses.Infrastructure.Services;

public class ExpenseDataService : IExpenseDataService
{
    private readonly IExpenseRepository _repository;

    public ExpenseDataService(IExpenseRepository repository)
    {
        _repository = repository;
    }

    public async Task<decimal> GetTotalExpensesAsync(DateTime startDate, DateTime endDate)
    {
        var expenses = await _repository.GetAllAsync();
        return expenses
            .Where(e => e.Date >= startDate && e.Date <= endDate)
            .Sum(e => e.Amount);
    }

    public async Task<IEnumerable<object>> GetExpenseBreakdownAsync()
    {
        var expenses = await _repository.GetAllAsync();
        return expenses
            .GroupBy(e => e.Category.Name)
            .Select(g => new { Category = g.Key, Total = g.Sum(e => e.Amount) })
            .ToList<object>();
    }

    public async Task<IEnumerable<object>> GetExpenseReportAsync(DateTime? start, DateTime? end)
    {
        var expenses = await _repository.GetAllAsync();
        var query = expenses.AsQueryable();

        if (start.HasValue) query = query.Where(e => e.Date >= start.Value);
        if (end.HasValue) query = query.Where(e => e.Date <= end.Value);

        return query.Select(e => new { e.Id, e.Description, e.Amount, e.Date, Category = e.Category.Name }).ToList<object>();
    }
}
