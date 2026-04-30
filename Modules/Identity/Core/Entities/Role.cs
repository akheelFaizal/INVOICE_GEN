using System;

namespace InvoiceSystem.Identity.Core.Entities;

public class Role
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
    public List<RolePermission> RolePermissions { get; set; }
}
