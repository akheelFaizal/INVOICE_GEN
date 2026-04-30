using InvoiceSystem.Shared;

namespace InvoiceSystem.Identity.Application.DTOs;

public record LoginRequest(string Email, string Password);
public record RegisterRequest(string Email, string Password, string FullName);
public record AuthResponse(string Token, string RefreshToken, DateTime Expiry);
public record UserResponse(Guid Id, string Email, string FullName, List<string> Roles);
public record RefreshTokenRequest(string RefreshToken);
public record UpdateUserRequest(string FullName, List<string> Roles);

