using Common;
using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers(Pagination pagination = null, Ordering ordering = null);

        Task<User> AddUser(User user);

        Task<User> GetUserById(int userId);

        Task SaveAsync();
    }
}
