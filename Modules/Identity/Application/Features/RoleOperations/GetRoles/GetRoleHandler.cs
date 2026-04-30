using System;
using InvoiceSystem.Identity.Application.Features.RoleOperations.Interfaces;
using InvoiceSystem.Identity.Core.Entities;

namespace InvoiceSystem.Identity.Application.Features.RoleOperations.GetRoles;

public class GetRoleHandler
{
    private readonly IRoleInterface _roleInterface;
    public GetRoleHandler(IRoleInterface roleInterface)
    {
        _roleInterface = roleInterface;        
    }
    public async Task<List<Role>> Handle()
    {
        return await _roleInterface.GetRoles();
    }
}
