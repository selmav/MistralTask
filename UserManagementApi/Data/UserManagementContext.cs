using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class UserManagementContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }

        public UserManagementContext(DbContextOptions<UserManagementContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("name=UserManagement");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>().HasData(Seed.PermissionSeed);
        }
    }
}
