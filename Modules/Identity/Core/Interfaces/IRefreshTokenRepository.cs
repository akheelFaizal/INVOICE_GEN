using System;
using InvoiceSystem.Identity.Core.Entities;

namespace InvoiceSystem.Identity.Core.Interfaces;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken token);

    Task<RefreshToken> GetByToken(string token);

    Task RevokeToken(RefreshToken token);
}
