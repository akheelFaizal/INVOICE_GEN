using System;

namespace InvoiceSystem.Identity.Core.Entities;

public class Permission
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
}
