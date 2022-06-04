using Core;
using Common;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace UserManagementApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<BaseResponse<PagedList<UserDto>>> GetUsers([FromQuery] Pagination pagination)
        {
            var users = await _userService.GetAllUsers(pagination);
            return BaseResponse<PagedList<UserDto>>.Ok(users);
        }
    }
}
