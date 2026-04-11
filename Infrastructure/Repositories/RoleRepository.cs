using System;
using Application.Features.Permission.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RoleRepository : IRoleInterface
{
    private readonly AppDbContext _context;
    public RoleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddRole(Role role)
    {
        await _context.Roles.AddAsync(role);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Role>> GetRoles()
    {
        var roles = await _context.Roles.ToListAsync();
        return roles;
    }

    public async Task<string> UpdateRole(Role role)
    {
        var existingRole = await _context.Roles.FindAsync(role.Id);
        if (existingRole == null)
        {
            return "Role not found.";
        }
        existingRole.Name = role.Name;
        await _context.SaveChangesAsync();
        return "Role updated successfully.";
    }

    public async Task<string> DeleteRole(Role role)
    {
        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();
        return "Role deleted successfully.";
    }
}
