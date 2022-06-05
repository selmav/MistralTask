using System.Collections.Generic;

namespace Data.Entities
{
    public class Permission
    {
        public int PermissionId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UserPermission> UserPermissions { get; set; }
    }
}
