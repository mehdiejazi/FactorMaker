﻿using Models;
using System.Threading.Tasks;

namespace Data.Repositories.RepositoryInterfaces
{
    public interface IUserRepository : Data.Base.IRepository<User>
    {
        User GetByUserName(string userName);
        Task<User> GetByUserNameAsync(string userName);

    }
}
