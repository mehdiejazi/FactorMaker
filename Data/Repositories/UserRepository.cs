using Data.Repositories.Base;
using Data.Repositories.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
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

        public User GetByUserName(string userName)
        {
            var user = DbSet.Where(u => u.UserName.ToLower() == userName.ToLower()).FirstOrDefault();
            return user;
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            var user = await DbSet.Where(u => u.UserName.ToLower() == userName.ToLower())
                .FirstOrDefaultAsync();
            return user;
        }
    }
}
