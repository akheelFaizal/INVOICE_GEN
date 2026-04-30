using Microsoft.AspNetCore.Mvc;
using InvoiceSystem.Invoicing.Core.Interfaces;
using InvoiceSystem.Invoicing.Core.DTOs;

namespace InvoiceSystem.Invoicing.API.Controllers;

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
    public async Task<IActionResult> Get()
    {
        var result = await _invoiceService.GetInvoicesAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateInvoiceRequest request)
    {
        var result = await _invoiceService.CreateInvoiceAsync(request);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }
}
