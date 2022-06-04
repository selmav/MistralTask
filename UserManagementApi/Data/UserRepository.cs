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
        private readonly UserManagementContext _context;

        public UserRepository(UserManagementContext context)
        {
            _context = context;
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

        public async Task<User> AddUser(User user)
        {
            var res = (await _table.AddAsync(user)).Entity;
            await SaveAsync();
            return res;
        }

        public async Task<User> GetUserById(int userId) => await _table.FindAsync(userId);

        public async Task SaveAsync() => await _context.SaveChangesAsync();

        public async Task DeleteUser(User user)
        {
            _table.Remove(user);
            await SaveAsync();
        }
    }
}
