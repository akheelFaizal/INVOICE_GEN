using System;

namespace Domain.Entities;

public class UserRole
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    
    public string RoleId { get; set; }
    public Role Role { get; set; }
}
