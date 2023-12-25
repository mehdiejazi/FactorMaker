using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories.RepositoryInterfaces
{
    public interface IFactorRepository : Data.Base.IRepository<Factor>
    {
        Task<ICollection<Factor>> GetByOwnerIdAsync(Guid ownerId);
        Factor GetWithItemsById(Guid id);
        Task<Factor> GetWithItemsByIdAsync(Guid id);
    }
}
