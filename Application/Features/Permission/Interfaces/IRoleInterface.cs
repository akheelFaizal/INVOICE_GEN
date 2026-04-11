using System;
using Domain.Entities;
namespace Application.Features.Permission.Interfaces;

public interface IRoleInterface
{ 
    public Task AddRole(Role role);
    public Task<List<Role>> GetRoles();
    public Task<string> UpdateRole(Role role);
    public Task<string> DeleteRole(Role role);
    
}
