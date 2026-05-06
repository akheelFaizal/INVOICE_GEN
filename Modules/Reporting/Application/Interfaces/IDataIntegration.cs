using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceSystem.Reporting.Application.Interfaces;

public interface IInvoiceDataService
{
    Task<decimal> GetTotalRevenueAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<object>> GetTopClientsAsync(int count);
    Task<IEnumerable<object>> GetInvoiceReportAsync(DateTime? start, DateTime? end, Guid? clientId, string? status);
}

public interface IExpenseDataService
{
    Task<decimal> GetTotalExpensesAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<object>> GetExpenseBreakdownAsync();
    Task<IEnumerable<object>> GetExpenseReportAsync(DateTime? start, DateTime? end);
}
