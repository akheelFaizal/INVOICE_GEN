using System;
using System.Security.Cryptography.Pkcs;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

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
