using System;

namespace InvoiceSystem.Identity.Application.Features.RoleOperations.DeleteRole;

public class DeleteRoleCommand
{
    public Guid RoleId { get; set; }
}
