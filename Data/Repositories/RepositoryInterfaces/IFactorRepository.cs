using Data.DataTransferObjects.Factor;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories.RepositoryInterfaces
{
    public interface IFactorRepository : Data.Base.IRepository<Factor>
    {
        Task<ICollection<Factor>> GetByOwnerIdAsync(Guid ownerId);
        Task<ICollection<Factor>> GetByStoreIdAsync(Guid storeId);
        Factor GetWithItemsById(Guid id);
        Task<Factor> GetWithItemsByIdAsync(Guid id);
        Task<int> GetCountByDateTimeStoreIdAsync(DateTime dtFrom, DateTime dtTo, Guid storeId);
        Task<decimal> GetSumTotalPriceByDateTimeStoreIdAsync(DateTime dtFrom, DateTime dtTo, Guid storeId);
        Task<ICollection<FactorSaleMonthlyDto>> GetMonthlyFactorSaleAsync(DateTime dtFrom, DateTime dtTo, Guid storeId);
        Task<ICollection<FactorSaleWeeklyDto>> GetWeeklyFactorSaleAsync(DateTime dtFrom, DateTime dtTo, Guid storeId);
        Task<ICollection<FactorSaleHourlyDto>> GetHourlyFactorSaleAsync(DateTime dtFrom, DateTime dtTo, Guid storeId);
        Task<ICollection<FactorSaleWeekDailyDto>> GetWeekDailyFactorSaleAsync
            (DateTime dtFrom, DateTime dtTo, Guid storeId);

    }
}
