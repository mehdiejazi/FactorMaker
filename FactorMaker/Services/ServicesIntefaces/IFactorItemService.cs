using Common;
using System;
using System.Threading.Tasks;
using ViewModels.FactorItem;

namespace FactorMaker.Services.ServiceIntefaces
{
    public interface IFactorItemService
    {
        Task<Result<FactorItemViewModel>> InsertAsync(FactorItemViewModel viewModel);
        Task<Result<FactorItemViewModel>> UpdateAsync(FactorItemViewModel viewModel);
        Task<Result> DeleteByIdAsync(Guid id);
        Task<Result<FactorItemViewModel>> GetByIdAsync(Guid id);
    }
}