using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InvoiceSystem.Invoicing.Application.Interfaces;
using InvoiceSystem.Invoicing.Application.DTOs;
using InvoiceSystem.Shared;
using System;
using System.Threading.Tasks;

namespace InvoiceSystem.Invoicing.API.Controllers;

[Authorize(Roles = "Admin,Accountant")]
[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;

    public PaymentsController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<PaymentResponse>>> GetById(Guid id)
    {
        var result = await _invoiceService.GetPaymentByIdAsync(id);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }
}
