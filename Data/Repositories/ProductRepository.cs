using Data.Repositories.Base;
using Data.Repositories.RepositoryInterfaces;
using Models;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        internal ProductRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }


    }
}
