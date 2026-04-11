using System;
using System.Security.Principal;
using Application.Features.Authentication.DTO;
using Application.Features.Authentication.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Shared.Helpers;
namespace Application.Features.Authentication.Login;

public class LoginHandler
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    
    public LoginHandler(IUserRepository userRepository, ITokenService tokenService, IRefreshTokenRepository refreshTokenRepository)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<TokenResponse> Handle(LoginCommand command)
    {
        var existing = await _userRepository.GetUserByEmail(command.Email);
        if(existing == null)
        {
            return new TokenResponse
            {
                AccessToken = "...",
                RefreshToken = "..."
            };
        }

        var isPasswordValid = PasswordHasher.VerifyPassword(command.Password, existing.PasswordHash);
        if(!isPasswordValid)
        {
            return new TokenResponse
            {
                AccessToken = "...",
                RefreshToken = "..."
            };
        }
        
        var token = _tokenService.GenerateToken(existing);

        var refreshToken = _tokenService.GenerateRefreshToken();
        var refreshTokenEntity = new RefreshToken
        {
            Token = refreshToken,
            UserId = existing.Id,
            ExpiryDate = DateTime.UtcNow.AddDays(7),
            User = existing 
        };

        await _refreshTokenRepository.AddAsync(refreshTokenEntity);

        return new TokenResponse
        {
            AccessToken = token,
            RefreshToken = refreshToken
        };
    
    }


}
