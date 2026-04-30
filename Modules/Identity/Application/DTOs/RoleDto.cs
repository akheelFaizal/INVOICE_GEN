using InvoiceSystem.Shared;

namespace InvoiceSystem.Identity.Application.DTOs;

public record RoleResponse(Guid Id, string Name, List<string> Permissions);
public record CreateRoleRequest(string Name);
public record PermissionResponse(Guid Id, string Name, string Description);
public record AssignPermissionsRequest(List<Guid> PermissionIds);
public record UpdateRoleRequest(string Name);

