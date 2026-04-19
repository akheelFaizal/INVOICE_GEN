using System.Security.Cryptography.X509Certificates;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{ 
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddUser(User user)
    {
        await _context.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetById(Guid Id)
    {
        return await _context.Users.FindAsync(Id);
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await _context.Users
        .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
                .ThenInclude(r => r.RolePermissions)
                        .ThenInclude(rp => rp.Permission)
                            .FirstOrDefaultAsync(x => x.Email == email);
    } 
 }
