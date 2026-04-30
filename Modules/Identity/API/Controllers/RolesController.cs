using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InvoiceSystem.Identity.Application.Interfaces;
using InvoiceSystem.Identity.Application.DTOs;
using InvoiceSystem.Shared;

namespace InvoiceSystem.Identity.API.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<ActionResult<Result<IEnumerable<RoleResponse>>>> GetAll()
    {
        var result = await _roleService.GetAllRolesAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<RoleResponse>>> GetById(Guid id)
    {
        var result = await _roleService.GetRoleByIdAsync(id);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Result<RoleResponse>>> Create(CreateRoleRequest request)
    {
        var result = await _roleService.CreateRoleAsync(request);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Result<RoleResponse>>> Update(Guid id, UpdateRoleRequest request)
    {
        var result = await _roleService.UpdateRoleAsync(id, request);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpDelete("{id}")]

    public async Task<ActionResult<Result>> Delete(Guid id)
    {
        var result = await _roleService.DeleteRoleAsync(id);
        return Ok(result);
    }

    [HttpGet("{id}/permissions")]
    public async Task<ActionResult<Result<IEnumerable<PermissionResponse>>>> GetPermissions(Guid id)
    {
        var result = await _roleService.GetRolePermissionsAsync(id);
        return Ok(result);
    }

    [HttpPost("{id}/permissions")]
    public async Task<ActionResult<Result>> AssignPermissions(Guid id, AssignPermissionsRequest request)
    {
        var result = await _roleService.AssignPermissionsToRoleAsync(id, request.PermissionIds);
        return Ok(result);
    }
}

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/permissions")]
public class PermissionsController : ControllerBase
{
    private readonly IRoleService _roleService;

    public PermissionsController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<ActionResult<Result<IEnumerable<PermissionResponse>>>> GetAll()
    {
        var result = await _roleService.GetAllPermissionsAsync();
        return Ok(result);
    }
}
