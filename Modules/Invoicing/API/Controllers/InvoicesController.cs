using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InvoiceSystem.Invoicing.Application.Interfaces;
using InvoiceSystem.Invoicing.Application.DTOs;
using InvoiceSystem.Shared;

namespace InvoiceSystem.Invoicing.API.Controllers;

[Authorize(Roles = "Admin,Accountant")]
[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;

    public InvoicesController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpGet]
    public async Task<ActionResult<Result<IEnumerable<InvoiceResponse>>>> Get()
    {
        var result = await _invoiceService.GetInvoicesAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Result<InvoiceResponse>>> Create(CreateInvoiceRequest request)
    {
        var result = await _invoiceService.CreateInvoiceAsync(request);
        if (!result.Success) return BadRequest(result);
        
        return CreatedAtAction(nameof(Get), new { id = result.Data.Id }, result);
    }
}
