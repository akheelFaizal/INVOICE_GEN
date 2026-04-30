using System;
using InvoiceSystem.Identity.Core.Entities;
namespace InvoiceSystem.Identity.Application.Features.RoleOperations.Interfaces;

public interface IRoleInterface
{ 
    public Task AddRole(Role role);
    public Task<List<Role>> GetRoles();
    public Task<string> UpdateRole(Role role);
    public Task<string> DeleteRole(Role role);
    public Task<string> AssignRoleToUser(Guid userId, Guid roleId);
    
}
