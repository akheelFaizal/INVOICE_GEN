using System;
using InvoiceSystem.Identity.Application.Features.RoleOperations.Interfaces;
using InvoiceSystem.Identity.Core.Entities;
using InvoiceSystem.Identity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystem.Identity.Infrastructure.Repositories;

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

    public async Task<string> AssignRoleToUser(Guid userId, Guid roleId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return "User not found.";
        }

        var role = await _context.Roles.FindAsync(roleId);
        if (role == null)
        {
            return "Role not found.";
        }

        var userRole = new UserRole
        {
            UserId = userId,
            RoleId = roleId
        };
        await _context.UserRoles.AddAsync(userRole);
        await _context.SaveChangesAsync();
        return "Role assigned to user successfully.";
    }
}
