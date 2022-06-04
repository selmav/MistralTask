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
        public async Task<BaseResponse<PagedList<UserDto>>> GetUsers([FromQuery] Pagination pagination, [FromQuery] Ordering ordering)
        {
            var users = await _userService.GetAllUsers(pagination, ordering);
            return BaseResponse<PagedList<UserDto>>.Ok(users);
        }

        [HttpPost]
        public async Task<BaseResponse<UserDto>> AddUser([FromBody] UserDto userDto)
        {
            var user = await _userService.AddUser(userDto);
            return BaseResponse<UserDto>.Ok(user);
        }
    }
}
