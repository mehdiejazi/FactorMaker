using Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Data.Repositories.RepositoryInterfaces
{
    public interface IStoreRepository : Data.Base.IRepository<Store>
    {
        Task<ICollection<Store>> GetByOwnerIdAsync(Guid ownerId);
        Task<Store> GetByStoreIdAsync(Guid storeId);
    }
}
