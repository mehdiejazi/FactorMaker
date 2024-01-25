using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories.RepositoryInterfaces
{
    public interface IPostCategoryRepository : Data.Base.IRepository<PostCategory>
    {
        Task<ICollection<PostCategory>> GetByOwnerIdAsync(Guid ownerId);
    }
}
