using System;
using InvoiceSystem.Identity.Application.Features.RoleOperations.Interfaces;
using InvoiceSystem.Identity.Application.Features.RoleOperations.RoleFeature;
using InvoiceSystem.Identity.Core.Entities;

namespace InvoiceSystem.Identity.Application.Features.RoleOperations.UpdateRole;

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
