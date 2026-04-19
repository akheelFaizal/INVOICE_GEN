using System;
using Application.Features.RoleOperations.Interfaces;

namespace Application.Features.RoleOperations.AssignRole;

public class AssignRoleHandler
{
    private readonly IRoleInterface _roleInterface;
    public AssignRoleHandler(IRoleInterface roleInterface)
    {
        _roleInterface = roleInterface;        
    }
    public async Task<string> Handle(AssignRoleCommand command)
    {
        var result = await _roleInterface.AssignRoleToUser(command.UserId, command.RoleId);
        return result;
    }
}
