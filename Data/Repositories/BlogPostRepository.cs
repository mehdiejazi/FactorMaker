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
    public class BlogPostRepository : RepositoryBase<BlogPost>, IBlogPostRepository
    {
        internal BlogPostRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public new async Task<BlogPost> GetByIdAsync(Guid Id)
        {
            var result = await DbSet
                .Where(x => x.Id == Id)
                    .Include(x => x.Images)
                    .Include(x => x.PostCategories)
                    .Include(x => x.CoverImage)
                    .Include(x => x.Owner)
                        .ThenInclude(u => u.Avatar)
                    .FirstOrDefaultAsync();

            return result;
        }

        public async Task<ICollection<BlogPost>> GetByOwnerIdAsync(Guid ownerId)
        {
            var result = await DbSet
              .Where(x => x.OwnerId.Equals(ownerId) && x.IsDeleted  == false)
                  .Include(x => x.Images)
                  .Include(x => x.PostCategories)
                  .Include(x => x.CoverImage)
                  .Include(x => x.Owner)
                      .ThenInclude(u => u.Avatar)
                  .ToListAsync();

            return result;
        }

        public async Task<ICollection<BlogPost>> GetByOwnerNotPublishedAsync(Guid ownerId)
        {
            var result = await DbSet
              .Where(x => x.OwnerId.Equals(ownerId) && x.IsPublished == false && x.IsDeleted == false)
                  .Include(x => x.Images)
                  .Include(x => x.PostCategories)
                  .Include(x => x.CoverImage)
                  .Include(x => x.Owner)
                      .ThenInclude(u => u.Avatar)
                  .ToListAsync();

            return result;
        }

        public async Task<ICollection<BlogPost>> GetByOwnerPublishedAsync(Guid ownerId)
        {
            var result = await DbSet
              .Where(x => x.OwnerId.Equals(ownerId) && x.IsPublished && x.IsDeleted == false)
                  .Include(x => x.Images)
                  .Include(x => x.PostCategories)
                  .Include(x => x.CoverImage)
                  .Include(x => x.Owner)
                      .ThenInclude(u => u.Avatar)
                  .ToListAsync();

            return result;
        }

        public async Task<ICollection<BlogPost>> GetByOwnerPublishedHotAsync(Guid ownerId)
        {
            var result = await DbSet
              .Where(x => x.OwnerId.Equals(ownerId) && x.IsPublished && x.IsHot && x.IsDeleted == false)
                  .Include(x => x.Images)
                  .Include(x => x.PostCategories)
                  .Include(x => x.CoverImage)
                  .Include(x => x.Owner)
                      .ThenInclude(u => u.Avatar)
                  .ToListAsync();

            return result;
        }

        public async Task<ICollection<BlogPost>> GetByPostCategoryAsync(PostCategory postCategory)
        {
            var result = await DbSet
              .Where(x => x.PostCategories.Any(pc => pc == postCategory) && x.IsDeleted == false)
                  .Include(x => x.Images)
                  .Include(x => x.PostCategories)
                  .Include(x => x.CoverImage)
                  .Include(x => x.Owner)
                      .ThenInclude(u => u.Avatar)
                  .ToListAsync();

            return result;
        }

        public async Task<ICollection<BlogPost>> GetPublishedAsync()
        {
            var result = await DbSet
             .Where(x => x.IsPublished && x.IsDeleted == false)
                 .Include(x => x.Images)
                 .Include(x => x.PostCategories)
                 .Include(x => x.CoverImage)
                 .Include(x => x.Owner)
                     .ThenInclude(u => u.Avatar)
                 .ToListAsync();

            return result;
        }

        public async Task<ICollection<BlogPost>> GetPublishedHotAsync()
        {
            var result = await DbSet
             .Where(x => x.IsPublished && x.IsHot && x.IsDeleted == false)
                 .Include(x => x.Images)
                 .Include(x => x.PostCategories)
                 .Include(x => x.CoverImage)
                 .Include(x => x.Owner)
                     .ThenInclude(u => u.Avatar)
                 .ToListAsync();

            return result;
        }

        public async Task<ICollection<BlogPost>> GetsNotPublishedAsync()
        {
            var result = await DbSet
             .Where(x => x.IsPublished && x.IsDeleted == false)
                 .Include(x => x.Images)
                 .Include(x => x.PostCategories)
                 .Include(x => x.CoverImage)
                 .Include(x => x.Owner)
                     .ThenInclude(u => u.Avatar)
                 .ToListAsync();

            return result;
        }
    }
}
