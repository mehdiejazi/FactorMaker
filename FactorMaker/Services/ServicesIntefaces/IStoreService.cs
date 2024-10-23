using Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Store;

namespace FactorMaker.Services.ServiceIntefaces
{
    public interface IStoreService
    {
        Task<Result<StoreViewModel>> InsertAsync(StoreViewModel viewModel);
        Task<Result<StoreViewModel>> UpdateAsync(StoreViewModel viewModel);
        Task<Result> DeleteByIdAsync(Guid id);
        Task<Result<StoreViewModel>> GetByIdAsync(Guid id);
        Task<Result<ICollection<StoreViewModel>>> GetByOwnerIdAsync(Guid ownerId);
        Task<Result<StoreViewModel>> GetByStoreEnglishNameAsync(string storeEnglishName);
    }
}