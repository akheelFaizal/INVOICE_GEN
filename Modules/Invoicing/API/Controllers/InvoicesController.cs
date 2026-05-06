using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InvoiceSystem.Invoicing.Application.Interfaces;
using InvoiceSystem.Invoicing.Application.DTOs;
using InvoiceSystem.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    public async Task<ActionResult<Result<IEnumerable<InvoiceResponse>>>> GetAll()
    {
        var result = await _invoiceService.GetInvoicesAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<InvoiceResponse>>> GetById(Guid id)
    {
        var result = await _invoiceService.GetInvoiceByIdAsync(id);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Result<InvoiceResponse>>> Create(CreateInvoiceRequest request)
    {
        var result = await _invoiceService.CreateInvoiceAsync(request);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Result<InvoiceResponse>>> Update(Guid id, UpdateInvoiceRequest request)
    {
        var result = await _invoiceService.UpdateInvoiceAsync(id, request);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Result>> Delete(Guid id)
    {
        var result = await _invoiceService.DeleteInvoiceAsync(id);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }

    // Invoice Items
    [HttpPost("{id}/items")]
    public async Task<ActionResult<Result<InvoiceItemResponse>>> AddItem(Guid id, InvoiceItemRequest request)
    {
        var result = await _invoiceService.AddItemAsync(id, request);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpPut("{id}/items/{itemId}")]
    public async Task<ActionResult<Result<InvoiceItemResponse>>> UpdateItem(Guid id, Guid itemId, InvoiceItemRequest request)
    {
        var result = await _invoiceService.UpdateItemAsync(id, itemId, request);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpDelete("{id}/items/{itemId}")]
    public async Task<ActionResult<Result>> RemoveItem(Guid id, Guid itemId)
    {
        var result = await _invoiceService.RemoveItemAsync(id, itemId);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    // Status Update
    [HttpPatch("{id}/status")]
    public async Task<ActionResult<Result>> UpdateStatus(Guid id, UpdateStatusRequest request)
    {
        var result = await _invoiceService.UpdateStatusAsync(id, request);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }

    // Payments
    [HttpGet("{id}/payments")]
    public async Task<ActionResult<Result<IEnumerable<PaymentResponse>>>> GetPayments(Guid id)
    {
        var result = await _invoiceService.GetPaymentsAsync(id);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }

    [HttpPost("{id}/payments")]
    public async Task<ActionResult<Result<PaymentResponse>>> AddPayment(Guid id, PaymentRequest request)
    {
        var result = await _invoiceService.AddPaymentAsync(id, request);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }
}
