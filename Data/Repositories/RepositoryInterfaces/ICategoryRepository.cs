using Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Data.Repositories.RepositoryInterfaces
{
    public interface ICategoryRepository : Data.Base.IRepository<Category>
    {
        Task<ICollection<Category>> GetByOwnerIdAsync(Guid ownerId);
    }
}
