using System;
using InvoiceSystem.Identity.Application.Features.RoleOperations.Interfaces;
using InvoiceSystem.Identity.Core.Entities;

namespace InvoiceSystem.Identity.Application.Features.RoleOperations.DeleteRole;

public class DeleteRoleHandler
{
    private readonly IRoleInterface _roleInterface;
    public DeleteRoleHandler(IRoleInterface roleInterface)
    {
        _roleInterface = roleInterface;        
    }
    public async Task<string> Handle(DeleteRoleCommand command)
    {
        var role = new Role
        {
            Id = command.RoleId
        };
        var result = await _roleInterface.DeleteRole(role);
        return result;
    }
}
