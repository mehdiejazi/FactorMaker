using Data.Repositories.Base;
using Data.Repositories.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class PostCategoryRepository : RepositoryBase<PostCategory>, IPostCategoryRepository
    {
        internal PostCategoryRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<ICollection<PostCategory>> GetByOwnerIdAsync(Guid ownerId)
        {
            var result = await
                DbSet
                .Where(current => current.OwnerId.Equals(ownerId))
                .ToListAsync();

            return result;
        }
    }
}
