using Models;
using System;
using System.Threading.Tasks;

namespace Data.Repositories.RepositoryInterfaces
{
    public interface IFactorRepository : Data.Base.IRepository<Factor>
    {
        Factor GetFactorWithItemsById(Guid id);
        Task<Factor> GetFactorWithItemsByIdAsync(Guid id);
    }
}
