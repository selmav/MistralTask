using Common;
using Core.Models;
using System.Threading.Tasks;

namespace Core
{
    public interface IUserService
    {
        Task<PagedList<UserDto>> GetAllUsers(Pagination pagination, Ordering ordering);

        Task<UserDto> AddUser(UserDto user);

        Task<UserDto> GetUserById(int? userId);

        Task<UserDto> UpdateUser(UserDto user);

        Task<bool> DeleteUser(int? userId);
    }
}
