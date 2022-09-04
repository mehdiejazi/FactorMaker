using Data.Base;
using Models.Tools;
using System;

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

    }
}
