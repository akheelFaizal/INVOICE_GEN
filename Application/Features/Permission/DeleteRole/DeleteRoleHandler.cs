using System;
using Application.Features.Permission.Interfaces;
using Domain.Entities;

namespace Application.Features.Permission.DeleteRole;

public class DeleteRoleHandler
{
    private readonly IRoleInterface _roleInterface;
    public DeleteRoleHandler(IRoleInterface roleInterface)
    {
        _roleInterface = roleInterface;        
    }
    public async Task<string> Handle(DeleteRoleCommand command)
    {
        var role = new Role
        {
            Id = command.RoleId
        };
        var result = await _roleInterface.DeleteRole(role);
        return result;
    }
}
