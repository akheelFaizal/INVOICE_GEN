using System.Security.Cryptography;
using System.Text;
using InvoiceSystem.Identity.Application.DTOs;
using InvoiceSystem.Identity.Application.Features.Authentication.Interfaces;
using InvoiceSystem.Identity.Application.Interfaces;
using InvoiceSystem.Identity.Core.Entities;
using InvoiceSystem.Identity.Core.Interfaces;
using InvoiceSystem.Shared;

namespace InvoiceSystem.Identity.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public AuthService(
        IUserRepository userRepository,
        ITokenService tokenService,
        IRefreshTokenRepository refreshTokenRepository)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<Result<AuthResponse>> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetUserByEmail(request.Email);
        if (user == null)
            return Result<AuthResponse>.FailureResult("Invalid email or password.");

        if (!VerifyPassword(request.Password, user.PasswordHash))
            return Result<AuthResponse>.FailureResult("Invalid email or password.");

        var token = _tokenService.GenerateToken(user);
        var refreshTokenStr = _tokenService.GenerateRefreshToken();

        var refreshToken = new RefreshToken
        {
            Token = refreshTokenStr,
            UserId = user.Id,
            User = user,
            ExpiryDate = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };

        await _refreshTokenRepository.AddAsync(refreshToken);

        var roles = user.UserRoles?.Select(ur => ur.Role.Name) ?? new List<string>();

        var userResponse = new UserResponse(user.Id, user.Email, user.Name, roles);
        var authResponse = new AuthResponse(token, refreshTokenStr, userResponse);

        return Result<AuthResponse>.SuccessResult(authResponse);
    }

    public async Task<Result<AuthResponse>> RegisterAsync(RegisterRequest request)
    {
        var existingUser = await _userRepository.GetUserByEmail(request.Email);
        if (existingUser != null)
            return Result<AuthResponse>.FailureResult("Email is already in use.");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            Name = request.FullName,
            PasswordHash = HashPassword(request.Password),
            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.AddUser(user);

        var token = _tokenService.GenerateToken(user);
        var refreshTokenStr = _tokenService.GenerateRefreshToken();

        var refreshToken = new RefreshToken
        {
            Token = refreshTokenStr,
            UserId = user.Id,
            User = user,
            ExpiryDate = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };

        await _refreshTokenRepository.AddAsync(refreshToken);

        var userResponse = new UserResponse(user.Id, user.Email, user.Name, new List<string>());
        var authResponse = new AuthResponse(token, refreshTokenStr, userResponse);

        return Result<AuthResponse>.SuccessResult(authResponse);
    }

    public Task<Result> LogoutAsync()
    {
        // Logout typically involves revoking the current refresh token and clearing client-side tokens.
        // For a stateless API, client-side clearance is usually enough, unless we keep a blacklist.
        return Task.FromResult(Result.SuccessResult());
    }

    public Task<Result<UserResponse>> GetMeAsync()
    {
        // This requires accessing the current user context (e.g. via IHttpContextAccessor).
        // Since we don't have it injected here, it's generally handled at the controller level
        // or by passing the user ID. Leaving as not implemented for now, or returning a generic failure.
        return Task.FromResult(Result<UserResponse>.FailureResult("Use User.Identity in controller"));
    }

    public async Task<Result<AuthResponse>> RefreshTokenAsync(string token)
    {
        var storedToken = await _refreshTokenRepository.GetByToken(token);
        if (storedToken == null || storedToken.IsRevoked || storedToken.ExpiryDate <= DateTime.UtcNow)
        {
            return Result<AuthResponse>.FailureResult("Invalid or expired refresh token.");
        }

        var user = await _userRepository.GetById(storedToken.UserId);
        if (user == null)
            return Result<AuthResponse>.FailureResult("User not found.");

        await _refreshTokenRepository.RevokeToken(storedToken);

        var newToken = _tokenService.GenerateToken(user);
        var newRefreshTokenStr = _tokenService.GenerateRefreshToken();

        var newRefreshToken = new RefreshToken
        {
            Token = newRefreshTokenStr,
            UserId = user.Id,
            User = user,
            ExpiryDate = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };

        await _refreshTokenRepository.AddAsync(newRefreshToken);

        var roles = user.UserRoles?.Select(ur => ur.Role.Name) ?? new List<string>();
        var userResponse = new UserResponse(user.Id, user.Email, user.Name, roles);
        var authResponse = new AuthResponse(newToken, newRefreshTokenStr, userResponse);

        return Result<AuthResponse>.SuccessResult(authResponse);
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    private bool VerifyPassword(string password, string hash)
    {
        return HashPassword(password) == hash;
    }
}
