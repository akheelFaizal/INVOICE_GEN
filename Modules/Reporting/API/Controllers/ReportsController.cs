using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InvoiceSystem.Reporting.Application.Services;
using InvoiceSystem.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceSystem.Reporting.API.Controllers;

[Authorize(Roles = "Admin,Accountant")]
[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly ReportService _reportService;

    public ReportsController(ReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("financial-summary")]
    public async Task<ActionResult<Result<object>>> GetSummary()
    {
        var result = await _reportService.GetFinancialSummaryAsync();
        return Ok(result);
    }

    [HttpGet("invoices")]
    public async Task<ActionResult<Result<IEnumerable<object>>>> GetInvoices(
        [FromQuery] DateTime? startDate, 
        [FromQuery] DateTime? endDate, 
        [FromQuery] Guid? clientId, 
        [FromQuery] string? status)
    {
        var result = await _reportService.GetInvoiceReportAsync(startDate, endDate, clientId, status);
        return Ok(result);
    }
}

[Authorize(Roles = "Admin,Accountant")]
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly AnalyticsService _analyticsService;

    public AnalyticsController(AnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    [HttpGet("revenue-vs-expenses")]
    public async Task<ActionResult<Result<object>>> GetRevenueVsExpenses()
    {
        var result = await _analyticsService.GetRevenueVsExpensesAsync();
        return Ok(result);
    }

    [HttpGet("top-clients")]
    public async Task<ActionResult<Result<IEnumerable<object>>>> GetTopClients()
    {
        var result = await _analyticsService.GetTopClientsAsync();
        return Ok(result);
    }
}
