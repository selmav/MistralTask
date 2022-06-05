using Common;
using Data.Entities;
using Data.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Implementation
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

        public async Task<IEnumerable<User>> GetUsers(
            Pagination pagination = null,
            Ordering ordering = null,
            Filtering filters = null)
        {
            Expression<System.Func<User, string>> order = ordering.Key switch
            {
                "firstName" => (User item) => item.FirstName,
                "lastName" => (User item) => item.LastName,
                "username" => (User item) => item.Username,
                "email" => (User item) => item.Email,
                "status" => (User item) => item.Status.Name,
                _ => null
            };
                

            return await DoFiltering(_table, filters)
                .Include(u => u.Status)
                .DoOrdering(order, ordering.Direction)
                .DoPaging(pagination)?.ToListAsync();
        }

        public async Task<User> AddUser(User user)
        {
            var res = (await _table.AddAsync(user)).Entity;
            await SaveAsync();
            return res;
        }

        public async Task<User> GetUserById(int userId) => 
            await _table.Include(u => u.UserPermissions)
                        .Include(u => u.Status)
                        .Where(u => u.UserId.Equals(userId)).FirstOrDefaultAsync();

        public async Task SaveAsync() => await _context.SaveChangesAsync();

        public async Task DeleteUser(User user)
        {
            _table.Remove(user);
            await SaveAsync();
        }

        private IQueryable<User> DoFiltering(IQueryable<User> source, Filtering filters)
        {
            if (filters == null ||
                (string.IsNullOrEmpty(filters.FirstName) &&
                 string.IsNullOrEmpty(filters.LastName) &&
                 string.IsNullOrEmpty(filters.UserName) &&
                 string.IsNullOrEmpty(filters.Email)))
            {
                return source;
            }

            return source.Where(item =>
                (string.IsNullOrEmpty(filters.FirstName) || item.FirstName.Contains(filters.FirstName)) &&
                (string.IsNullOrEmpty(filters.LastName) || item.LastName.Contains(filters.LastName)) &&
                (string.IsNullOrEmpty(filters.UserName) || item.Username.Contains(filters.UserName)) &&
                (string.IsNullOrEmpty(filters.Email) || item.Email.Contains(filters.Email))
            );
        }
    }
}
