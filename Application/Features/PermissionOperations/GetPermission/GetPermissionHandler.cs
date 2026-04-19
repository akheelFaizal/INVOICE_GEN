using System;
using Application.Features.PermissionOperations.Interfaces;
using Domain.Entities;

namespace Application.Features.PermissionOperations.GetPermission;

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
