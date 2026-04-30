using System;
using System.Security;
using InvoiceSystem.Identity.Application.Features.PermissionOperations.AssignPermission;
using InvoiceSystem.Identity.Application.Features.PermissionOperations.Interfaces;
using InvoiceSystem.Identity.Application.Features.RoleOperations.Interfaces;
using InvoiceSystem.Identity.Core.Entities;
using InvoiceSystem.Identity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystem.Identity.Infrastructure.Repositories;

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
