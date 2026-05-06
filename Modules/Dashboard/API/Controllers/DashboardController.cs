using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InvoiceSystem.Invoicing.Application.Interfaces;
using InvoiceSystem.Shared;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace InvoiceSystem.Dashboard.API.Controllers;

[Authorize(Roles = "Admin,Accountant")]
[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;

    public DashboardController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpGet("kpis")]
    public async Task<ActionResult<Result<object>>> GetKpis()
    {
        var summary = await _invoiceService.GetDashboardSummaryAsync();
        return Ok(summary);
    }

    [HttpGet("recent-invoices")]
    public async Task<ActionResult<Result<IEnumerable<object>>>> GetRecentInvoices()
    {
        var result = await _invoiceService.GetInvoicesAsync();
        // Logic to take top 5 most recent...
        return Ok(result);
    }
}

[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Result<IEnumerable<object>>>> Search([FromQuery] string query)
    {
        // Orchestrate search across modules...
        return Ok(Result<IEnumerable<object>>.SuccessResult(new List<object>()));
    }
}
