using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class UserManagementContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserManagementContext(DbContextOptions<UserManagementContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("name=UserManagement");
        }
    }
}
