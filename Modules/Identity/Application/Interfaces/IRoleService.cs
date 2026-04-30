using InvoiceSystem.Identity.Application.DTOs;
using InvoiceSystem.Shared;

namespace InvoiceSystem.Identity.Application.Interfaces;

public interface IRoleService
{
    Task<Result<IEnumerable<RoleResponse>>> GetAllRolesAsync();
    Task<Result<RoleResponse>> GetRoleByIdAsync(Guid id);
    Task<Result<RoleResponse>> CreateRoleAsync(CreateRoleRequest request);
    Task<Result<RoleResponse>> UpdateRoleAsync(Guid id, UpdateRoleRequest request);
    Task<Result> DeleteRoleAsync(Guid id);

    Task<Result<IEnumerable<PermissionResponse>>> GetAllPermissionsAsync();
    Task<Result> AssignPermissionsToRoleAsync(Guid roleId, List<Guid> permissionIds);
    Task<Result<IEnumerable<PermissionResponse>>> GetRolePermissionsAsync(Guid roleId);
}
