using System;
using System.Security.Cryptography.Pkcs;
using InvoiceSystem.Identity.Core.Entities;
using InvoiceSystem.Identity.Core.Interfaces;
using InvoiceSystem.Identity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystem.Identity.Infrastructure.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{

    private readonly AppDbContext _context;

    public RefreshTokenRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(RefreshToken token)
    {
        await _context.RefreshTokens.AddAsync(token);
        await _context.SaveChangesAsync();
    }

    public async Task<RefreshToken> GetByToken(string token)
    {
        return await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == token);
    }

    public async Task RevokeToken(RefreshToken token)
    {
        token.IsRevoked = true;
        await _context.SaveChangesAsync();
    }
}
