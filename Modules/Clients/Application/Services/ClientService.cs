using InvoiceSystem.Clients.Application.DTOs;
using InvoiceSystem.Clients.Application.Interfaces;
using InvoiceSystem.Clients.Core.Entities;
using InvoiceSystem.Clients.Core.Interfaces;
using InvoiceSystem.Shared;

namespace InvoiceSystem.Clients.Application.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IInvoiceIntegrationService _invoiceIntegrationService;

    public ClientService(IClientRepository clientRepository, IInvoiceIntegrationService invoiceIntegrationService)
    {
        _clientRepository = clientRepository;
        _invoiceIntegrationService = invoiceIntegrationService;
    }

    public async Task<Result<IEnumerable<ClientResponse>>> GetAllClientsAsync(Guid? currentUserId, string role)
    {
        var clients = await _clientRepository.GetAllAsync();
        
        // RBAC: Staff can only access clients they are assigned to
        if (role == "Staff")
        {
            clients = clients.Where(c => c.AssignedStaffId == currentUserId);
        }

        var response = clients.Select(MapToResponse);
        return Result<IEnumerable<ClientResponse>>.SuccessResult(response);
    }

    public async Task<Result<ClientResponse>> GetClientByIdAsync(Guid id, Guid? currentUserId, string role)
    {
        var client = await _clientRepository.GetByIdAsync(id);
        if (client == null) return Result<ClientResponse>.FailureResult("Client not found");

        // RBAC: Staff can only access clients they are assigned to
        if (role == "Staff" && client.AssignedStaffId != currentUserId)
        {
            return Result<ClientResponse>.FailureResult("Unauthorized access to this client");
        }

        return Result<ClientResponse>.SuccessResult(MapToResponse(client));
    }

    public async Task<Result<ClientResponse>> CreateClientAsync(CreateClientRequest request)
    {
        var client = new Client
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Address = request.Address,
            AssignedStaffId = request.AssignedStaffId,
            OutstandingBalance = 0,
            TotalBilled = 0
        };

        await _clientRepository.AddAsync(client);
        return Result<ClientResponse>.SuccessResult(MapToResponse(client));
    }

    public async Task<Result<ClientResponse>> UpdateClientAsync(Guid id, UpdateClientRequest request)
    {
        var client = await _clientRepository.GetByIdAsync(id);
        if (client == null) return Result<ClientResponse>.FailureResult("Client not found");

        client.Name = request.Name;
        client.Email = request.Email;
        client.PhoneNumber = request.PhoneNumber;
        client.Address = request.Address;
        client.AssignedStaffId = request.AssignedStaffId;

        await _clientRepository.UpdateAsync(client);
        return Result<ClientResponse>.SuccessResult(MapToResponse(client));
    }

    public async Task<Result> DeleteClientAsync(Guid id)
    {
        await _clientRepository.DeleteAsync(id);
        return Result.SuccessResult();
    }

    public async Task<Result<IEnumerable<object>>> GetClientInvoicesAsync(Guid id)
    {
        var invoices = await _invoiceIntegrationService.GetInvoicesByClientIdAsync(id);
        return Result<IEnumerable<object>>.SuccessResult(invoices);
    }

    private static ClientResponse MapToResponse(Client client)
    {
        return new ClientResponse(
            client.Id,
            client.Name,
            client.Email,
            client.PhoneNumber,
            client.Address,
            client.OutstandingBalance,
            client.TotalBilled,
            client.AssignedStaffId
        );
    }
}
