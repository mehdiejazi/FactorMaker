using Data.DataTransferObjects.Factor;
using Data.Repositories.Base;
using Data.Repositories.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class FactorRepository : RepositoryBase<Factor>, IFactorRepository
    {
        internal FactorRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<ICollection<Factor>> GetByOwnerIdAsync(Guid ownerId)
        {
            var result = await DbSet.Where(current => current.OwnerId.Equals(ownerId))
                              .Include(x => x.Owner)
                              .OrderByDescending(x => x.InsertDateTime)
                              .ToListAsync();

            return result;
        }

        public async Task<ICollection<Factor>> GetByStoreIdAsync(Guid storeId)
        {
            var result = await DbSet.Where(current => current.StoreId.Equals(storeId))
                              .Include(x => x.Owner)
                              .OrderByDescending(x => x.InsertDateTime)
                              .ToListAsync();

            return result;
        }

        public Factor GetWithItemsById(Guid id)
        {
            var result = DbSet
                            .Where(current => current.Id.Equals(id) && current.IsDeleted == false)
                            .Include(x => x.FactorItems)
                                .ThenInclude(i => i.Product)
                            .Include(x => x.Owner)
                            .OrderByDescending(x => x.InsertDateTime)
                            .FirstOrDefault();

            return result;
        }

        public async Task<Factor> GetWithItemsByIdAsync(Guid id)
        {
            var result = await DbSet
                            .Where(current => current.Id.Equals(id) && current.IsDeleted == false)

                            .Include(x => x.FactorItems)
                                .ThenInclude(i => i.Product)
                            .Include(x => x.Owner)
                            .OrderByDescending(x => x.InsertDateTime)
                            .FirstOrDefaultAsync();

            return result;
        }

        public async Task<int> GetCountByDateTimeStoreIdAsync(DateTime dtFrom, DateTime dtTo, Guid storeId)
        {
            var result = await DbSet
                            .Where(x => x.SellDateTime >= dtFrom
                                     && x.SellDateTime < dtTo
                                     && x.IsDeleted == false
                                     && x.IsClosed
                                     && x.StoreId.Equals(storeId))
                            .CountAsync();

            return result;
        }

        public async Task<decimal> GetSumTotalPriceByDateTimeStoreIdAsync(DateTime dtFrom, DateTime dtTo, Guid storeId)
        {
            var result = await DbSet
                            .Where(x => x.SellDateTime >= dtFrom
                                     && x.SellDateTime < dtTo
                                     && x.IsDeleted == false
                                     && x.IsClosed
                                     && x.StoreId.Equals(storeId))
                            .Select(x => x.TotalPrice)
                            .SumAsync();

            return result;
        }

        public async Task<ICollection<FactorSaleMonthlyDto>> GetMonthlyFactorSaleAsync
            (DateTime dtFrom, DateTime dtTo, Guid storeId)
        {
            PersianCalendar persianCalendar = new PersianCalendar();

            var monthlySales = await DbSet
                        .Where(f => f.IsClosed &&
                                    f.SellDateTime >= dtFrom &&
                                    f.SellDateTime < dtTo &&
                                    f.StoreId.Equals(storeId) &&
                                    f.IsDeleted == false)
                        .GroupBy(f => new
                        {
                            Year = persianCalendar.GetYear(f.SellDateTime),
                            Month = persianCalendar.GetMonth(f.SellDateTime)
                        })
                        .Select(group => new FactorSaleMonthlyDto
                        {
                            Year = group.Key.Year,
                            Month = group.Key.Month,
                            Count = group.Count(),
                            TotalPrice = group.Sum(x => x.TotalPrice)
                        })
                        .OrderBy(result => result.Year)
                        .ThenBy(result => result.Month)
                        .ToListAsync();

            return monthlySales;
        }

        public async Task<ICollection<FactorSaleWeeklyDto>> GetWeeklyFactorSaleAsync
            (DateTime dtFrom, DateTime dtTo, Guid storeId)
        {
            PersianCalendar persianCalendar = new PersianCalendar();

            var weeklySales = await DbSet
                        .Where(f => f.IsClosed &&
                                    f.SellDateTime >= dtFrom &&
                                    f.SellDateTime < dtTo &&
                                    f.StoreId.Equals(storeId) &&
                                    f.IsDeleted == false)
                        .GroupBy(f => new
                        {
                            Year = persianCalendar.GetYear(f.SellDateTime),
                            Week = persianCalendar.GetWeekOfYear(f.SellDateTime, CalendarWeekRule.FirstDay, DayOfWeek.Saturday),
                        })
                        .Select(group => new FactorSaleWeeklyDto
                        {
                            Year = group.Key.Year,
                            Week = group.Key.Week,
                            Count = group.Count(),
                            TotalPrice = group.Sum(x => x.TotalPrice)
                        })
                        .OrderBy(result => result.Year)
                        .ThenBy(result => result.Week)
                        .ToListAsync();

            return weeklySales;
        }

        public async Task<ICollection<FactorSaleHourlyDto>> GetHourlyFactorSaleAsync
             (DateTime dtFrom, DateTime dtTo, Guid storeId)
        {
            var hourlySales = await DbSet
                 .Where(f => f.IsClosed &&
                             f.SellDateTime >= dtFrom &&
                             f.SellDateTime < dtTo &&
                             f.StoreId.Equals(storeId) &&
                             f.IsDeleted == false)
               .GroupBy(f => f.SellDateTime.Hour)
               .Select(group => new FactorSaleHourlyDto
               {
                   Hour = group.Key,
                   Count = group.Count(),
                   TotalPrice = group.Sum(f => f.TotalPrice)
               })
               .OrderBy(result => result.Hour)
               .ToListAsync();

            return hourlySales;
        }

        public async Task<ICollection<FactorSaleWeekDailyDto>> GetWeekDailyFactorSaleAsync
            (DateTime dtFrom, DateTime dtTo, Guid storeId)
        {
            PersianCalendar persianCalendar = new PersianCalendar();

            var hourlySales = await DbSet
                 .Where(f => f.IsClosed &&
                             f.SellDateTime < dtTo &&
                             f.SellDateTime >= dtFrom &&
                             f.StoreId.Equals(storeId) &&
                             f.IsDeleted == false)
               .GroupBy(f => new { DayOfWeek = persianCalendar.GetDayOfWeek(f.SellDateTime) })
               .Select(group => new FactorSaleWeekDailyDto
               {
                   DayOfWeek = group.Key.DayOfWeek,
                   Count = group.Count(),
                   TotalPrice = group.Sum(f => f.TotalPrice)
               })
               .OrderBy(result => result.DayOfWeek)
               .ToListAsync();

            return hourlySales;
        }


    }
}
