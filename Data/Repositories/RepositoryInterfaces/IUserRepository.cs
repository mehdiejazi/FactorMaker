using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories.RepositoryInterfaces
{
    public interface IUserRepository : Data.Base.IRepository<User>
    {
        Task<User> GetByUserNameAsync(string userName);
        Task<ICollection<User>> GetActiveAsync();
        Task<ICollection<User>> GetInActiveAsync();
        Task<bool> IsExistByUsernameAsync(string userName);
        Task<bool> HasAccessToStoreAsync(Guid id, Guid storeId);
        Task<User> GetByRefreshTokenAsync(string refreshToken);
    }
}
