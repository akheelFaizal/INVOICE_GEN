using System;

namespace Domain.Entities;

public class Permission
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
}
