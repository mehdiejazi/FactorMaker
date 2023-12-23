using Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace Data.Repositories.RepositoryInterfaces
{
    public interface IProductRepository : Data.Base.IRepository<Product>
    {
        Task<ICollection<Product>> GetByOwnerIdCategoryIdAsync(Guid ownerId, Guid categoryId);
    }
}
