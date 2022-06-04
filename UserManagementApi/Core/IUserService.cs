using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsers();
    }
}
