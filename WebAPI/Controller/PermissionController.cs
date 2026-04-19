using Application.Features.PermissionOperations.AddPermission;
using Application.Features.PermissionOperations.AssignPermission;
using Application.Features.PermissionOperations.GetPermission;
using Application.Features.RoleOperations.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly AddPermissionHandler _addPermissionHandler;
        private readonly GetPermissionHandler _getPermissionHandler;
        private readonly AssignPermissionHandler _assignPermissionHandler;
        public PermissionController(AddPermissionHandler addPermissionHandler, GetPermissionHandler getPermissionHandler, AssignPermissionHandler assignPermissionHandler)
        {
            _addPermissionHandler = addPermissionHandler;
            _getPermissionHandler = getPermissionHandler;
            _assignPermissionHandler = assignPermissionHandler;
        }
        

        [HttpPost("add")]
        public async Task<IActionResult> AddPermission(AddPermissionCommand command)
        {
            var result = await _addPermissionHandler.Handle(command);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetPermissions()
        {
            var permissions = await _getPermissionHandler.Handle();
            return Ok(permissions);
        }

        [HttpPost("assign/{roleId}/permission/{permissionId}")]
        public async Task<IActionResult> AssignPermission(Guid roleId, Guid permissionId)
        {
            var command = new AssignPermissionCommand
            {
                RoleId = roleId,
                PermissionId = permissionId
            };
            var result = await _assignPermissionHandler.Handle(command);
            return Ok(result);
        }

    }
}
