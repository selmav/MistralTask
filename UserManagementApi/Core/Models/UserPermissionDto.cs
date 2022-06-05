using System.Collections.Generic;

namespace Core.Models
{
    public class UserPermissionDto
    {
        public int UserId { get; set; }

        public IEnumerable<int> PermissionIds { get; set; }
    }
}
