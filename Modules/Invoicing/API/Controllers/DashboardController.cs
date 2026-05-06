using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InvoiceSystem.Invoicing.Application.Interfaces;
using InvoiceSystem.Invoicing.Application.DTOs;
using InvoiceSystem.Shared;
using System.Threading.Tasks;

namespace InvoiceSystem.Invoicing.API.Controllers;

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

    [HttpGet("summary")]
    public async Task<ActionResult<Result<DashboardSummaryResponse>>> GetSummary()
    {
        var result = await _invoiceService.GetDashboardSummaryAsync();
        return Ok(result);
    }
}
