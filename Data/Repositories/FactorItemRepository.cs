using Data.Repositories.Base;
using Data.Repositories.RepositoryInterfaces;
using Models;

namespace Data.Repositories
{
    public class FactorItemRepository : RepositoryBase<FactorItem>, IFactorItemRepository
    {
        internal FactorItemRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        
    }
}
