using Common;
using Data.DataTransferObjects.Factor;
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
        Task<Result<TotalFactorViewModel>> CalculateFactorByIdAsync(Guid id);
        Task<Result<FactorViewModel>> GetFactorWithItemsByIdAsync(Guid id);
        Task<Result<ICollection<FactorViewModel>>> GetByStoreIdAsync(Guid storeId);
        Task<Result<ICollection<FactorSaleMonthlyViewModel>>> GetMonthlyFactorSaleAsync(int year, Guid storeId);
        Task<Result<ICollection<FactorSaleWeeklyViewModel>>> GetWeeklyFactorSaleAsync(int year, int month, Guid storeId);
        Task<Result<ICollection<FactorSaleHourlyViewModel>>> GetHourlyFactorSaleAsync
            (DateTime dtFrom, DateTime dtTo, Guid storeId);
        Task<Result<ICollection<FactorSaleWeekDailyViewModel>>> GetWeekDailyFactorSaleAsync
            (DateTime dtFrom, DateTime dtTo, Guid storeId);
    }
}