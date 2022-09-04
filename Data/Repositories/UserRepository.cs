using Data.Repositories.Base;
using Data.Repositories.RepositoryInterfaces;
using Models;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        internal UserRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }


    }
}
