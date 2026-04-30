using System;
using InvoiceSystem.Identity.Application.Features.RoleOperations.Interfaces;
using InvoiceSystem.Identity.Core.Entities;

namespace InvoiceSystem.Identity.Application.Features.RoleOperations.RoleFeature;

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
