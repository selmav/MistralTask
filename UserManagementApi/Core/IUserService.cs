using Common;
using Core.Models;
using System.Threading.Tasks;

namespace Core
{
    public interface IUserService
    {
        Task<PagedList<UserDto>> GetAllUsers(Pagination pagination);
    }
}
