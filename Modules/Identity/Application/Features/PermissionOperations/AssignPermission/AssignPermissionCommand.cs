using System;

namespace InvoiceSystem.Identity.Application.Features.PermissionOperations.AssignPermission;

public class AssignPermissionCommand
{
    public Guid RoleId {get; set;}
    public Guid PermissionId {get; set;}
}
