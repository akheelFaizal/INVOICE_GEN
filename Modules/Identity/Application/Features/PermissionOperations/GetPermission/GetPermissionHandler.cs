using System;
using InvoiceSystem.Identity.Application.Features.PermissionOperations.Interfaces;
using InvoiceSystem.Identity.Core.Entities;

namespace InvoiceSystem.Identity.Application.Features.PermissionOperations.GetPermission;

public class GetPermissionHandler
{
    private readonly IPermissionInterface _permissionInterface;
    public GetPermissionHandler(IPermissionInterface permissionInterface)
    {
        _permissionInterface = permissionInterface;        
    }

    public async Task<List<Permission>> Handle()
    {
        return await _permissionInterface.GetPermissions();
    }
}
