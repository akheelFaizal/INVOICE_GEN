using System;
using InvoiceSystem.Identity.Application.Features.PermissionOperations.Interfaces;

namespace InvoiceSystem.Identity.Application.Features.PermissionOperations.AssignPermission;

public class AssignPermissionHandler
{
    private readonly IPermissionInterface _permissionInterface;
    public AssignPermissionHandler(IPermissionInterface permissionInterface)
    {
        _permissionInterface = permissionInterface;        
    }
    public async Task<string> Handle(AssignPermissionCommand command)
    {
        var result = await _permissionInterface.AssignPermission(command);
        return result;
    }
}
