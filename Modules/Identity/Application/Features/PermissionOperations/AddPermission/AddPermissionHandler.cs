using System;
using InvoiceSystem.Identity.Application.Features.PermissionOperations.Interfaces;
using InvoiceSystem.Identity.Core.Entities;

namespace InvoiceSystem.Identity.Application.Features.PermissionOperations.AddPermission;

public class AddPermissionHandler
{

    private readonly IPermissionInterface _permissionInterface;
    public AddPermissionHandler(IPermissionInterface permissionInterface)
    {
        _permissionInterface = permissionInterface;        
    }
    public async Task<string> Handle(AddPermissionCommand command)
    {
        var permission = new Permission
        {
            Name = command.Name
        };
        var result = await _permissionInterface.AddPermission(permission);
        return result;
    }
    
}
