using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
    }
}
