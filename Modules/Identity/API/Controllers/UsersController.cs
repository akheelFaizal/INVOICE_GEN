using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InvoiceSystem.Identity.Application.Interfaces;
using InvoiceSystem.Identity.Application.DTOs;
using InvoiceSystem.Shared;

namespace InvoiceSystem.Identity.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize(Roles = "Admin,Accountant")]
    [HttpGet]
    public async Task<ActionResult<Result<IEnumerable<UserResponse>>>> GetAll()
    {
        var result = await _userService.GetAllUsersAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<UserResponse>>> GetById(Guid id)
    {
        var result = await _userService.GetUserByIdAsync(id);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<Result<UserResponse>>> Create(RegisterRequest request)
    {
        var result = await _userService.CreateUserAsync(request);
        if (!result.Success) return BadRequest(result);
        return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<Result<UserResponse>>> Update(Guid id, UpdateUserRequest request)
    {
        var result = await _userService.UpdateUserAsync(id, request);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]

    [HttpDelete("{id}")]
    public async Task<ActionResult<Result>> Delete(Guid id)
    {
        var result = await _userService.DeleteUserAsync(id);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("{id}/status")]
    public async Task<ActionResult<Result>> UpdateStatus(Guid id, [FromBody] bool isActive)
    {
        var result = await _userService.UpdateUserStatusAsync(id, isActive);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }
}
