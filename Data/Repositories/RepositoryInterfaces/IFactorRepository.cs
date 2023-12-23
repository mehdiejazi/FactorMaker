using Models;
using System;
using System.Threading.Tasks;

namespace Data.Repositories.RepositoryInterfaces
{
    public interface IFactorRepository : Data.Base.IRepository<Factor>
    {
        Factor GetWithItemsById(Guid id);
        Task<Factor> GetWithItemsByIdAsync(Guid id);
    }
}
