using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InvoiceSystem.Reporting.Application.Interfaces;
using InvoiceSystem.Shared;

namespace InvoiceSystem.Reporting.Application.Services;

public class ReportService
{
    private readonly IInvoiceDataService _invoiceData;
    private readonly IExpenseDataService _expenseData;

    public ReportService(IInvoiceDataService invoiceData, IExpenseDataService expenseData)
    {
        _invoiceData = invoiceData;
        _expenseData = expenseData;
    }

    public async Task<Result<object>> GetFinancialSummaryAsync()
    {
        var now = DateTime.UtcNow;
        var startOfMonth = new DateTime(now.Year, now.Month, 1);
        
        var revenue = await _invoiceData.GetTotalRevenueAsync(startOfMonth, now);
        var expenses = await _expenseData.GetTotalExpensesAsync(startOfMonth, now);
        
        return Result<object>.SuccessResult(new {
            Period = "Current Month",
            TotalRevenue = revenue,
            TotalExpenses = expenses,
            NetProfit = revenue - expenses
        });
    }

    public async Task<Result<IEnumerable<object>>> GetInvoiceReportAsync(DateTime? start, DateTime? end, Guid? clientId, string? status)
    {
        var data = await _invoiceData.GetInvoiceReportAsync(start, end, clientId, status);
        return Result<IEnumerable<object>>.SuccessResult(data);
    }
}

public class AnalyticsService
{
    private readonly IInvoiceDataService _invoiceData;
    private readonly IExpenseDataService _expenseData;

    public AnalyticsService(IInvoiceDataService invoiceData, IExpenseDataService expenseData)
    {
        _invoiceData = invoiceData;
        _expenseData = expenseData;
    }

    public async Task<Result<object>> GetRevenueVsExpensesAsync()
    {
        // For last 6 months
        // Logic to aggregate monthly trends...
        return Result<object>.SuccessResult(new { Message = "Analytical trend data here" });
    }

    public async Task<Result<IEnumerable<object>>> GetTopClientsAsync()
    {
        var clients = await _invoiceData.GetTopClientsAsync(5);
        return Result<IEnumerable<object>>.SuccessResult(clients);
    }
}
