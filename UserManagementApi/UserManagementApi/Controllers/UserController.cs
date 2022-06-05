using Common;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Core.Interface;

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
        public async Task<BaseResponse<PagedList<UserDto>>> GetUsers(
            [FromQuery] Pagination pagination,
            [FromQuery] Ordering ordering,
            [FromQuery] Filtering filters)
        {
            var users = await _userService.GetAllUsers(pagination, ordering, filters);
            return BaseResponse<PagedList<UserDto>>.Ok(users);
        }

        [HttpPost]
        public async Task<BaseResponse<UserDto>> AddUser([FromBody] UserDto userDto)
        {
            var user = await _userService.AddUser(userDto);
            return BaseResponse<UserDto>.Ok(user);
        }

        [HttpGet("{userId}")]
        public async Task<BaseResponse<UserDto>> GetUserById([FromRoute] int? userId)
        {
            var user = await _userService.GetUserById(userId);
            return BaseResponse<UserDto>.Ok(user);
        }

        [HttpPut]
        public async Task<BaseResponse<UserDto>> UpdateUser([FromBody] UserDto userDto)
        {
            var user = await _userService.UpdateUser(userDto);
            return BaseResponse<UserDto>.Ok(user);
        }

        [HttpDelete("{userId}")]
        public async Task<BaseResponse<UserDto>> DeleteUser([FromRoute] int? userId)
        {
            await _userService.DeleteUser(userId);
            return BaseResponse<UserDto>.Ok();
        }

        [HttpGet("statuses")]
        public async Task<BaseResponse<IEnumerable<StatusDto>>> GetUserStatuses()
        {
            var statuses = await _userService.GetUserStatuses();
            return BaseResponse<IEnumerable<StatusDto>>.Ok(statuses);
        }
    }
}
