using Data.Repositories.Base;
using Data.Repositories.RepositoryInterfaces;
using Models;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class FactorItemRepository : RepositoryBase<FactorItem>, IFactorItemRepository
    {
        internal FactorItemRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }


    }
}
