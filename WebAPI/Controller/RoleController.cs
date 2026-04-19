using Application.Features.RoleOperations.AssignRole;
using Application.Features.RoleOperations.DeleteRole;
using Application.Features.RoleOperations.GetRoles;
using Application.Features.RoleOperations.Interfaces;
using Application.Features.RoleOperations.RoleFeature;
using Application.Features.RoleOperations.UpdateRole;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleHandler _roleHandler;
        private readonly GetRoleHandler _getRoleHandler;
        private readonly UpdateRoleHandler _updateRoleHandler;
        private readonly DeleteRoleHandler _deleteRoleHandler;
        private readonly AssignRoleHandler _assignRoleHandler;
        public RoleController(RoleHandler roleHandler, GetRoleHandler getRoleHandler, UpdateRoleHandler updateRoleHandler, DeleteRoleHandler deleteRoleHandler, AssignRoleHandler assignRoleHandler)
        {
            _roleHandler = roleHandler;
            _getRoleHandler = getRoleHandler;
            _updateRoleHandler = updateRoleHandler;
            _deleteRoleHandler = deleteRoleHandler;
            _assignRoleHandler = assignRoleHandler;
        }
        
        [HttpPost("create")]
        public async Task<IActionResult> CreateRole(RoleCommand command)
        {
            var result = await _roleHandler.Handle(command);
            return Ok(result);
        } 

        [HttpGet("all")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _getRoleHandler.Handle();
            return Ok(roles);
        }

        [HttpPut("update/{roleId}")]
        public async Task<IActionResult> UpdateRole(Guid roleId, RoleCommand command)
        {
            var result = await _updateRoleHandler.Handle(roleId, command);
            return Ok(result);
        }

        [HttpDelete("delete/{roleId}")]
        public async Task<IActionResult> DeleteRole(Guid roleId)
        {
            var command = new DeleteRoleCommand { RoleId = roleId };
            var result = await _deleteRoleHandler.Handle(command);
            return Ok(result);
        }

        [HttpPost("assign/{roleId}/user/{userId}")]
        public async Task<IActionResult> AssignRole(Guid roleId, Guid userId)
        {
            var command = new AssignRoleCommand { RoleId = roleId, UserId = userId };
            var result = await _assignRoleHandler.Handle(command);
            return Ok(result);
        }
    }
}
