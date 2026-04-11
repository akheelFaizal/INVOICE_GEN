using System;
using Application.Features.Permission.Interfaces;
using Domain.Entities;

namespace Application.Features.Permission.RoleFeature;

public class RoleHandler
{
    private readonly IRoleInterface _roleInterface;
    public RoleHandler(IRoleInterface roleInterface)
    {
        _roleInterface = roleInterface;        
    }

    public async Task<string> Handle(RoleCommand command)
    {
        var role = new Role
        {
            Name = command.Name
        };
        await _roleInterface.AddRole(role);
        return $"Role '{role.Id}' has been created successfully.";
    }
}
