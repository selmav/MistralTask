using Common;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DbSet<User> _table;
        public UserRepository(UserManagementContext context)
        {
            _table = context.Set<User>();
        }

        public async Task<IEnumerable<User>> GetUsers(Pagination pagination = null)
        {
            return await _table.DoPaging(pagination).ToListAsync();
        }
    }
}
