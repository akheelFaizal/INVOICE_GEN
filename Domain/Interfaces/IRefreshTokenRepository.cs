using System;
using Domain.Entities;

namespace Domain.Interfaces;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken token);

    Task<RefreshToken> GetByToken(string token);

    Task RevokeToken(RefreshToken token);
}
