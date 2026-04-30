using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using InvoiceSystem.Clients.Application.Interfaces;
using InvoiceSystem.Clients.Application.DTOs;
using InvoiceSystem.Shared;

namespace InvoiceSystem.Clients.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<ActionResult<Result<IEnumerable<ClientResponse>>>> GetAll()
    {
        var userId = GetCurrentUserId();
        var role = GetCurrentUserRole();
        var result = await _clientService.GetAllClientsAsync(userId, role);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<ClientResponse>>> GetById(Guid id)
    {
        var userId = GetCurrentUserId();
        var role = GetCurrentUserRole();
        var result = await _clientService.GetClientByIdAsync(id, userId, role);
        if (!result.Success) return result.Error.Contains("Unauthorized") ? Forbid() : NotFound(result);
        return Ok(result);
    }

    [Authorize(Roles = "Admin,Accountant")]
    [HttpPost]
    public async Task<ActionResult<Result<ClientResponse>>> Create(CreateClientRequest request)
    {
        var result = await _clientService.CreateClientAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result);
    }

    [Authorize(Roles = "Admin,Accountant")]
    [HttpPut("{id}")]
    public async Task<ActionResult<Result<ClientResponse>>> Update(Guid id, UpdateClientRequest request)
    {
        var result = await _clientService.UpdateClientAsync(id, request);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }

    [Authorize(Roles = "Admin,Accountant")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<Result>> Delete(Guid id)
    {
        var result = await _clientService.DeleteClientAsync(id);
        return Ok(result);
    }

    [HttpGet("{id}/invoices")]
    public async Task<ActionResult<Result<IEnumerable<object>>>> GetInvoices(Guid id)
    {
        var result = await _clientService.GetClientInvoicesAsync(id);
        return Ok(result);
    }

    private Guid? GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.TryParse(userIdClaim, out var userId) ? userId : null;
    }

    private string GetCurrentUserRole()
    {
        return User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
    }
}
