using Models;
using System;
using System.Threading.Tasks;

namespace FactorMaker.Services.Base
{
    public class BaseServiceWithDatabase
    {
        public BaseServiceWithDatabase(Data.IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        protected Data.IUnitOfWork UnitOfWork { get; }

        protected async Task<bool> HasAccessUserToStore(User user,Guid storeId)
        {
            if (user.Role.Name == "Programmer")
            {
                return true;
            }

            return await UnitOfWork.UserRepository.HasAccessToStoreAsync(user.Id, storeId);
        }
    }
}
