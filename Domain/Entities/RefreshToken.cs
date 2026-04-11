using System;

namespace Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string Token { get; set; }

    public required Guid UserId { get; set; }

    public DateTime ExpiryDate { get; set; }

    public bool IsRevoked { get; set; } = false;

    public required User User { get; set; }
}
