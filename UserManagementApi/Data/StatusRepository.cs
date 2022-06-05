using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
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
