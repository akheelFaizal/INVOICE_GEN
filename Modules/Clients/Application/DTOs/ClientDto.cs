using InvoiceSystem.Shared;

namespace InvoiceSystem.Clients.Application.DTOs;

public record ClientResponse(
    Guid Id,
    string Name,
    string Email,
    string PhoneNumber,
    string Address,
    decimal OutstandingBalance,
    decimal TotalBilled,
    Guid? AssignedStaffId
);

public record CreateClientRequest(
    string Name,
    string Email,
    string PhoneNumber,
    string Address,
    Guid? AssignedStaffId
);

public record UpdateClientRequest(
    string Name,
    string Email,
    string PhoneNumber,
    string Address,
    Guid? AssignedStaffId
);
