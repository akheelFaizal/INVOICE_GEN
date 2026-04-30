using InvoiceSystem.Identity.Application.DTOs;
using InvoiceSystem.Shared;

namespace InvoiceSystem.Identity.Application.Interfaces;

public interface IAuthService
{
    Task<Result<AuthResponse>> LoginAsync(LoginRequest request);
    Task<Result<AuthResponse>> RegisterAsync(RegisterRequest request);
    Task<Result> LogoutAsync();
    Task<Result<UserResponse>> GetMeAsync();
    Task<Result<AuthResponse>> RefreshTokenAsync(string refreshToken);
}
