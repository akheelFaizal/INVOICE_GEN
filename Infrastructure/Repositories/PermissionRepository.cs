using System;
using System.Security;
using Application.Features.PermissionOperations.AssignPermission;
using Application.Features.PermissionOperations.Interfaces;
using Application.Features.RoleOperations.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PermissionRepository : IPermissionInterface
{
    private readonly AppDbContext _context;
    public PermissionRepository(AppDbContext context) 
    {
        _context = context;
    }

    public async Task<string> AddPermission(Permission permission)
    {
        await _context.Permissions.AddAsync(permission);
        await _context.SaveChangesAsync();
        return "Permission added successfully";
    }

    public async Task<List<Permission>> GetPermissions()
    {
        var result = await _context.Permissions.ToListAsync();
        return result;
    }

    public async Task<string> AssignPermission(AssignPermissionCommand command)
    {
        var role = await _context.Roles.FindAsync(command.RoleId) ?? throw new SecurityException("Role not found");
        var permission = await _context.Permissions.FindAsync(command.PermissionId) ?? throw new SecurityException("Permission not found");
        var rolePermission = new RolePermission
        {
            RoleId = command.RoleId,
            PermissionId = command.PermissionId
        };

        await  _context.RolePermissions.AddAsync(rolePermission);
        await _context.SaveChangesAsync();
        return "Permission assigned successfully";
    }

}
