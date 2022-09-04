using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Base
{
    public class Repository<T> : object, IRepository<T> where T : Models.Base.EntityBase
        //public abstract class Repository<T> : object, IRepository<T> where T : Models.Base.Entity
    {
        internal Repository(DatabaseContext databaseContext) : base()
        {
            // **************************************************
            DatabaseContext =
                databaseContext ?? throw new ArgumentNullException(paramName: nameof(databaseContext));
            // **************************************************

            DbSet = DatabaseContext.Set<T>();
        }

        // **********
        internal DatabaseContext DatabaseContext { get; }
        // **********

        // **********
        internal DbSet<T> DbSet { get; }
        // **********

        public virtual void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(paramName: nameof(entity));
            }

            DbSet.Add(entity);
        }

        public virtual async Task InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(paramName: nameof(entity));
            }

            await DbSet.AddAsync(entity);
        }

        public virtual void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(paramName: nameof(entity));
            }

            DbSet.Update(entity);

        }

        public virtual async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(paramName: nameof(entity));
            }

            //DbSet.Update(entity);

            await Task.Run(() =>
            {
                DbSet.Update(entity);
            });
        }

        public virtual void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(paramName: nameof(entity));
            }

            DbSet.Remove(entity);
        }

        public virtual async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(paramName: nameof(entity));
            }

            await Task.Run(() =>
            {
                DbSet.Remove(entity);
            });
        }

        public virtual T GetById(Guid id)
        {
            return DbSet.Find(keyValues: id);
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(keyValues: id);
        }

        public virtual bool DeleteById(Guid id)
        {
            T entity = GetById(id);

            if (entity == null)
            {
                return false;
            }

            Delete(entity);

            return true;
        }

        public virtual async Task<bool> DeleteByIdAsync(Guid id)
        {
            T entity =
                await GetByIdAsync(id);

            if (entity == null)
            {
                return false;
            }

            await DeleteAsync(entity);

            return true;
        }

        public virtual ICollection<T> GetAll()
        {
            var result =
                DbSet.
                OrderBy(x => x.InsertDateTime).ToList();

            return result;
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            var result =
                await
                DbSet.
                OrderBy(x => x.InsertDateTime).ToListAsync();

            return result;
        }
    }
}
