using InvoiceSystem.Identity.Application.DTOs;
using InvoiceSystem.Identity.Application.Features.PermissionOperations.Interfaces;
using InvoiceSystem.Identity.Application.Features.RoleOperations.Interfaces;
using InvoiceSystem.Identity.Application.Interfaces;
using InvoiceSystem.Identity.Core.Entities;
using InvoiceSystem.Shared;

namespace InvoiceSystem.Identity.Application.Services;

public class RoleService : IRoleService
{
    private readonly IRoleInterface _roleRepository;
    private readonly IPermissionInterface _permissionRepository;

    public RoleService(IRoleInterface roleRepository, IPermissionInterface permissionRepository)
    {
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
    }

    public async Task<Result<IEnumerable<RoleResponse>>> GetAllRolesAsync()
    {
        var roles = await _roleRepository.GetRoles();
        var responses = new List<RoleResponse>();

        foreach (var role in roles)
        {
            var rolePermissions = await _roleRepository.GetRolePermissionsAsync(role.Id);
            var permissionNames = rolePermissions.Select(rp => rp.Permission.Name).ToList();
            responses.Add(new RoleResponse(role.Id, role.Name, permissionNames));
        }

        return Result<IEnumerable<RoleResponse>>.SuccessResult(responses);
    }

    public async Task<Result<RoleResponse>> GetRoleByIdAsync(Guid id)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null)
            return Result<RoleResponse>.FailureResult("Role not found.");

        var rolePermissions = await _roleRepository.GetRolePermissionsAsync(role.Id);
        var permissionNames = rolePermissions.Select(rp => rp.Permission.Name).ToList();

        var response = new RoleResponse(role.Id, role.Name, permissionNames);
        return Result<RoleResponse>.SuccessResult(response);
    }

    public async Task<Result<RoleResponse>> CreateRoleAsync(CreateRoleRequest request)
    {
        var role = new Role
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };

        await _roleRepository.AddRole(role);

        var response = new RoleResponse(role.Id, role.Name, new List<string>());
        return Result<RoleResponse>.SuccessResult(response);
    }

    public async Task<Result<RoleResponse>> UpdateRoleAsync(Guid id, UpdateRoleRequest request)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null)
            return Result<RoleResponse>.FailureResult("Role not found.");

        role.Name = request.Name;
        await _roleRepository.UpdateRole(role);

        var rolePermissions = await _roleRepository.GetRolePermissionsAsync(role.Id);
        var permissionNames = rolePermissions.Select(rp => rp.Permission.Name).ToList();

        var response = new RoleResponse(role.Id, role.Name, permissionNames);
        return Result<RoleResponse>.SuccessResult(response);
    }

    public async Task<Result> DeleteRoleAsync(Guid id)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null)
            return Result.FailureResult("Role not found.");

        await _roleRepository.DeleteRole(role);
        return Result.SuccessResult();
    }

    public async Task<Result<IEnumerable<PermissionResponse>>> GetRolePermissionsAsync(Guid id)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null)
            return Result<IEnumerable<PermissionResponse>>.FailureResult("Role not found.");

        var rolePermissions = await _roleRepository.GetRolePermissionsAsync(id);
        var responses = rolePermissions.Select(rp => new PermissionResponse(rp.Permission.Id, rp.Permission.Name, rp.Permission.Name)).ToList();

        return Result<IEnumerable<PermissionResponse>>.SuccessResult(responses);
    }

    public async Task<Result> AssignPermissionsToRoleAsync(Guid id, List<Guid> permissionIds)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null)
            return Result.FailureResult("Role not found.");

        await _roleRepository.AssignPermissionsToRole(id, permissionIds);
        return Result.SuccessResult();
    }

    public async Task<Result<IEnumerable<PermissionResponse>>> GetAllPermissionsAsync()
    {
        var permissions = await _permissionRepository.GetPermissions();
        var responses = permissions.Select(p => new PermissionResponse(p.Id, p.Name, p.Name)).ToList();

        return Result<IEnumerable<PermissionResponse>>.SuccessResult(responses);
    }
}
