using Common;
using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core
{
    public interface IUserService
    {
        Task<PagedList<UserDto>> GetAllUsers(Pagination pagination, Ordering ordering, Filtering filters = null);

        Task<UserDto> AddUser(UserDto user);

        Task<UserDto> GetUserById(int? userId);

        Task<UserDto> UpdateUser(UserDto user);

        Task<bool> DeleteUser(int? userId);

        Task<IEnumerable<StatusDto>> GetUserStatuses();
    }
}
