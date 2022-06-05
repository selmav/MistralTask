using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface IStatusRepository
    {
        Task<IEnumerable<Status>> GetAll();
    }
}
