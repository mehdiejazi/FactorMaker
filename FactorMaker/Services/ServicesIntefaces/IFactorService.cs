using Common;
using Data.DataTransferObjects.Factor;
using Models;
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
        Task<Result<ICollection<FactorViewModel>>> GetByStoreIdAsync(User user,Guid storeId);
        Task<Result<ICollection<FactorSaleMonthlyViewModel>>> GetMonthlyFactorSaleAsync(User user,int year, Guid storeId);
        Task<Result<ICollection<FactorSaleWeeklyViewModel>>> GetWeeklyFactorSaleAsync
            (User user,int year, int month, Guid storeId);
        Task<Result<ICollection<FactorSaleHourlyViewModel>>> GetHourlyFactorSaleAsync
            (User user,DateTime dtFrom, DateTime dtTo, Guid storeId);
        Task<Result<ICollection<FactorSaleWeekDailyViewModel>>> GetWeekDailyFactorSaleAsync
            (User user,DateTime dtFrom, DateTime dtTo, Guid storeId);
    }
}