using Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Factor;

namespace FactorMaker.Services.ServicesIntefaces
{
    public interface IFactorService
    {
        Task<Result<FactorViewModel>> InsertAsync(FactorViewModel viewModel);
        Task<Result<FactorViewModel>> UpdateAsync(FactorViewModel viewModel);
        Task<Result> DeleteByIdAsync(Guid id);
        Task<Result<FactorViewModel>> GetByIdAsync(Guid id);
        Task<Result<ICollection<FactorViewModel>>> GetByOwnerIdAsync(Guid ownerId);
        Task<Result<ICollection<FactorViewModel>>> GetByCreatorIdAsync(Guid creatorId);
        Task<Result<TotalFactorViewModel>> CalculateFactorByIdAsync(Guid id);
        Task<Result<FactorViewModel>> GetFactorWithItemsByIdAsync(Guid id);
    }
}