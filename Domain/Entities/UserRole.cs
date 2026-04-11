using System;

namespace Domain.Entities;

public class UserRole
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string UserId { get; set; }
    public User User { get; set; }
    
    public string RoleId { get; set; }
    public Role Role { get; set; }
}
