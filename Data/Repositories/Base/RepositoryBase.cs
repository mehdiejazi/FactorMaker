using Data.Base;
using Microsoft.EntityFrameworkCore;
using Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories.Base
{
    public class RepositoryBase<T> : Repository<T> where T : Models.Base.EntityBase
    {
        internal RepositoryBase(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public override void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(paramName: nameof(entity));
            }

            entity.InsertDateTime = Utility.Now;

            DbSet.Add(entity);
        }

        public override void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(paramName: nameof(entity));
            }

            entity.UpdateDateTime = Utility.Now;

            DbSet.Update(entity);
        }

        public override void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(paramName: nameof(entity));
            }

            entity.DeleteDateTime = Utility.Now;
            entity.IsDeleted = true;

            DbSet.Update(entity);
        }

        public override async Task InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(paramName: nameof(entity));
            }

            entity.InsertDateTime = Utility.Now;

            await DbSet.AddAsync(entity);
        }

        public override async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(paramName: nameof(entity));
            }

            entity.UpdateDateTime = Utility.Now;

            await Task.Run(() =>
            {
                DbSet.Update(entity);
            });
        }

        public override async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(paramName: nameof(entity));
            }

            entity.DeleteDateTime = Utility.Now;
            entity.IsDeleted = true;

            await Task.Run(() =>
            {
                DbSet.Update(entity);
            });
        }

        public override bool DeleteById(Guid id)
        {
            var entity = GetById(id);

            if (entity == null)
            {
                return false;
            }

            Delete(entity);

            return true;
        }

        public async override Task<bool> DeleteByIdAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
            {
                return false;
            }

            await DeleteAsync(entity);

            return true;
        }

        public override ICollection<T> GetAll()
        {
            var result =
                DbSet.
                Where(x=>x.IsDeleted == false).
                OrderBy(x => x.InsertDateTime).ToList();

            return result;
        }

        public async override Task<ICollection<T>> GetAllAsync()
        {
            var result =
                await
                DbSet.
                Where(x => x.IsDeleted == false).
                OrderBy(x => x.InsertDateTime).ToListAsync();

            return result;
        }

    }
}
