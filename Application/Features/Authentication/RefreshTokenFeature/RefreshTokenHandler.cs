using System;
using Application.Features.Authentication.Interfaces;
using Domain.Interfaces;

namespace Application.Features.Authentication.RefreshTokenFeature;

public class RefreshTokenHandler
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    public RefreshTokenHandler(IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository, ITokenService tokenService)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<string> Handle(RefreshTokenCommand command)
    {
        var existingToken = await _refreshTokenRepository.GetByToken(command.RefreshToken);
        if (existingToken == null || existingToken.IsRevoked)
        {
            return "Invalid Token!";
        }

        if (existingToken.ExpiryDate < DateTime.UtcNow)
        {
            return "Token Expired!";
        }
        
        var user = await _userRepository.GetById(existingToken.UserId);
        var newAccessToken = _tokenService.GenerateToken(user);
        return newAccessToken;
    }

}
