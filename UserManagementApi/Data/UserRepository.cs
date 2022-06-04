using Common;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<IEnumerable<User>> GetUsers(Pagination pagination = null, Ordering ordering = null)
        {
            Expression<System.Func<User, string>> order = ordering.Key switch
            {
                "firstName" => (User item) => item.FirstName,
                "lastName" => (User item) => item.LastName,
                "username" => (User item) => item.Username,
                "Email" => (User item) => item.Email,
                "Status" => (User item) => item.Status,
                _ => ((User item) => string.Empty)
            };
                

            return await _table
                .DoOrdering(order, ordering.Direction)
                .DoPaging(pagination).ToListAsync();
        }
    }
}
