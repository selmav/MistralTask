using Data.Entities;
using Data.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Implementation
{
    public class StatusRepository : IStatusRepository
    {
        private readonly DbSet<Status> _table;

        public StatusRepository(UserManagementContext context)
        {
            _table = context.Set<Status>();
        }

        public async Task<IEnumerable<Status>> GetAll() => await _table.ToListAsync();
    }
}
