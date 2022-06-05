using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Common;
using Core.Interface;

namespace UserManagementApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;
        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        public async Task<BaseResponse<IEnumerable<PermissionDto>>> GetPermissions()
        {
            var permissions = await _permissionService.GetAllPermissions();
            return BaseResponse<IEnumerable<PermissionDto>>.Ok(permissions);
        }

        [HttpGet("user-permissions")]
        public async Task<BaseResponse<IEnumerable<PermissionDto>>> GetUserPermissions([FromQuery] int? userId)
        {
            var permissions = await _permissionService.GetUserPermissions(userId);
            return BaseResponse<IEnumerable<PermissionDto>>.Ok(permissions);
        }

        [HttpPut("user-permissions")]
        public async Task<BaseResponse<IEnumerable<int>>> UpdateUserPermissions([FromBody] UserPermissionDto userPermissions)
        {
            await _permissionService.UpdateUserPermissions(userPermissions);
            return BaseResponse<IEnumerable<int>>.Ok();
        }
    }
}
