using Data.Entities;
using System.Collections.Generic;

namespace Data
{
    public static class Seed
    {
        public static IEnumerable<Permission> PermissionSeed = new List<Permission>()
        {
            new Permission() { PermissionId = 1, Code = "Admin", Description = "Admin permission" },
            new Permission() { PermissionId = 2, Code = "Non-Admin", Description = "Non-admin permission" }
        };

        public static IEnumerable<Status> StatusSeed = new List<Status>()
        {
            new Status() { StatusId = 1, Name = "Active" },
            new Status() { StatusId = 2, Name = "Inactive" },
        };
    }
}
