using System;

namespace InvoiceSystem.Identity.Application.Features.RoleOperations.AssignRole;

public class AssignRoleCommand
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}
