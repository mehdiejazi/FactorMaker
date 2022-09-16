using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories.RepositoryInterfaces
{
    public interface IUserRepository : Data.Base.IRepository<User>
    {
        User GetByUserName(string userName);
        Task<User> GetByUserNameAsync(string userName);
        ICollection<User> GetActive();
        Task<ICollection<User>> GetActiveAsync();
        ICollection<User> GetInActive();
        Task<ICollection<User>> GetInActiveAsync();
    }
}
