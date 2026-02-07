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

        public new async Task<ICollection<PostCategory>> GetAllAsync()
        {
            var list = await DbSet
                .Include(p=>p.Owner)
                    .ThenInclude(u=>u.Avatar)
                .OrderByDescending(u => u.InsertDateTime)
                .ToListAsync();

            return list;
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
