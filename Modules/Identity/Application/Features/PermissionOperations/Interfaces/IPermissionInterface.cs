using System;
using InvoiceSystem.Identity.Application.Features.PermissionOperations.AssignPermission;
using InvoiceSystem.Identity.Core.Entities;

namespace InvoiceSystem.Identity.Application.Features.PermissionOperations.Interfaces;

public interface IPermissionInterface
{
    public Task<string> AddPermission(Permission permission);
    public Task<List<Permission>> GetPermissions();
    public Task<string> AssignPermission(AssignPermissionCommand command);
}
