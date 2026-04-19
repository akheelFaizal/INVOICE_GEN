using System;
using Application.Features.RoleOperations.Interfaces;
using Application.Features.RoleOperations.RoleFeature;
using Domain.Entities;

namespace Application.Features.RoleOperations.UpdateRole;

public class UpdateRoleHandler
{
    private readonly IRoleInterface _roleInterface;
    public UpdateRoleHandler(IRoleInterface roleInterface)
    {
        _roleInterface = roleInterface;        
    }

    public async Task<string> Handle(Guid roleId, RoleCommand command)
    {
        var role = new Role
        {
            Id = roleId,
            Name = command.Name
        };
        var result = await _roleInterface.UpdateRole(role);
        return result;
    }
}
