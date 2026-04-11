using Application.Features.Permission.DeleteRole;
using Application.Features.Permission.GetRoles;
using Application.Features.Permission.Interfaces;
using Application.Features.Permission.RoleFeature;
using Application.Features.Permission.UpdateRole;
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
        public RoleController(IRoleInterface roleInterface)
        {
            _roleHandler = new RoleHandler(roleInterface);
            _getRoleHandler = new GetRoleHandler(roleInterface);
            _updateRoleHandler = new UpdateRoleHandler(roleInterface);
            _deleteRoleHandler = new DeleteRoleHandler(roleInterface);
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
    }
}
