using Data.Repositories.Base;
using Data.Repositories.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class FactorRepository : RepositoryBase<Factor>, IFactorRepository
    {
        internal FactorRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public Factor GetWithItemsById(Guid id)
        {
            var result = DbSet
                            .Where(current => current.Id.Equals(id) && current.IsDeleted == false)
                            .Include(x => x.FactorItems)
                                .ThenInclude(i => i.Product)
                            .Include(x => x.Owner)
                            .Include(x => x.Creator)
                            .OrderByDescending(x => x.InsertDateTime)
                            .FirstOrDefault();

            return result;
        }

        public async Task<Factor> GetWithItemsByIdAsync(Guid id)
        {
            var result = await DbSet
                            .Where(current => current.Id.Equals(id) && current.IsDeleted == false)
                            .Include(x => x.FactorItems)
                                .ThenInclude(i => i.Product)
                            .Include(x => x.Owner)
                            .Include(x => x.Creator)
                            .OrderByDescending(x => x.InsertDateTime)
                            .FirstOrDefaultAsync();

            return result;
        }
    }
}
