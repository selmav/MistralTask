using Data.Entities;
using Data.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Implementation
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly DbSet<Permission> _table;
        private readonly UserManagementContext _context;

        public PermissionRepository(UserManagementContext context)
        {
            _context = context;
            _table = context.Set<Permission>();
        }

        public async Task<IEnumerable<Permission>> GetAllPermissions() =>
            await _table.ToListAsync();

        public async Task<IEnumerable<UserPermission>> GetUserPermissions(int userId)
        {
            var userPermissionTable = _context.Set<UserPermission>();
            return await userPermissionTable
                .Where(item => item.UserId.Equals(userId))
                .Include(item => item.Permission)
                .ToListAsync();
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();

        public async Task RemoveUserPermissions(int userId, IEnumerable<int> removed)
        {
            var table = _context.Set<UserPermission>();
            var userPermissions = await table.Where(p => p.UserId.Equals(userId) && removed.Contains(p.PermissionId))
                                             .ToListAsync();
            table.RemoveRange(userPermissions);
        }

        public async Task InsertPermissions(int userId, IEnumerable<int> permissionIds)
        {
            var table = _context.Set<UserPermission>();
            var permissions = permissionIds.Select(id => new UserPermission { PermissionId = id, UserId = userId });
            await table.AddRangeAsync(permissions);
        }
    }
}
