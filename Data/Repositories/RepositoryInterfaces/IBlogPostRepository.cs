using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories.RepositoryInterfaces
{
    public interface IBlogPostRepository : Data.Base.IRepository<BlogPost>
    {
        Task<ICollection<BlogPost>> GetPublishedAsync();

        Task<ICollection<BlogPost>> GetPublishedHotAsync();

        Task<ICollection<BlogPost>> GetsNotPublishedAsync();

        Task<ICollection<BlogPost>> GetByPostCategoryAsync(PostCategory postCategory);

        Task<ICollection<BlogPost>> GetByOwnerIdAsync(Guid ownerId);

        Task<ICollection<BlogPost>> GetByOwnerPublishedAsync(Guid ownerId);

        Task<ICollection<BlogPost>> GetByOwnerPublishedHotAsync(Guid ownerId);

        Task<ICollection<BlogPost>> GetByOwnerNotPublishedAsync(Guid ownerId);
    }
}
