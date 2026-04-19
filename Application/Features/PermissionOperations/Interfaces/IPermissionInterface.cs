using System;
using Application.Features.PermissionOperations.AssignPermission;
using Domain.Entities;

namespace Application.Features.PermissionOperations.Interfaces;

public interface IPermissionInterface
{
    public Task<string> AddPermission(Permission permission);
    public Task<List<Permission>> GetPermissions();
    public Task<string> AssignPermission(AssignPermissionCommand command);
}
