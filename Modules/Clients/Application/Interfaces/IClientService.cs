using InvoiceSystem.Clients.Application.DTOs;
using InvoiceSystem.Shared;

namespace InvoiceSystem.Clients.Application.Interfaces;

public interface IClientService
{
    Task<Result<IEnumerable<ClientResponse>>> GetAllClientsAsync(Guid? currentUserId, string role);
    Task<Result<ClientResponse>> GetClientByIdAsync(Guid id, Guid? currentUserId, string role);
    Task<Result<ClientResponse>> CreateClientAsync(CreateClientRequest request);
    Task<Result<ClientResponse>> UpdateClientAsync(Guid id, UpdateClientRequest request);
    Task<Result> DeleteClientAsync(Guid id);
    Task<Result<IEnumerable<object>>> GetClientInvoicesAsync(Guid id);
}
