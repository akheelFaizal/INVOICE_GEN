using System;
using Application.Features.Permission.Interfaces;
using Domain.Entities;

namespace Application.Features.Permission.GetRoles;

public class GetRoleHandler
{
    private readonly IRoleInterface _roleInterface;
    public GetRoleHandler(IRoleInterface roleInterface)
    {
        _roleInterface = roleInterface;        
    }
    public async Task<List<Role>> Handle()
    {
        return await _roleInterface.GetRoles();
    }
}
