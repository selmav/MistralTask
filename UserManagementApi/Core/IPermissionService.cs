using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core
{
    public interface IPermissionService
    {
        Task<IEnumerable<PermissionDto>> GetAllPermissions();

        Task<IEnumerable<PermissionDto>> GetUserPermissions(int? userId);

        Task<IEnumerable<int>> UpdateUserPermissions(UserPermissionDto userPermissions);
    }
}
