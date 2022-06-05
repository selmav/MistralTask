using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetAllPermissions();

        Task<IEnumerable<UserPermission>> GetUserPermissions(int userId);

        Task SaveAsync();

        Task InsertPermissions(int userId, IEnumerable<int> permissionIds);

        Task RemoveUserPermissions(int userId, IEnumerable<int> removed);
    }
}
