using System;
using System.Diagnostics;
using InvoiceSystem.Identity.Core.Interfaces;

namespace InvoiceSystem.Identity.Application.Features.Authentication.Logout;

public class LogoutHandler
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    public LogoutHandler(IRefreshTokenRepository refreshTokenRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<string> Handle(LogoutCommand command)
    {
        var existing = await _refreshTokenRepository.GetByToken(command.RefreshToken);
        if (existing == null)
        {
            return "Token Not Found!";
        }

        if (existing.IsRevoked)
        {
            return "Token Revoked Already!";
        }
        existing.IsRevoked = true;
        await _refreshTokenRepository.RevokeToken(existing);
        return "Token Revoked Succesfully!";
    }
}
