using Common;
using Data;
using Data.DataTransferObjects.Factor;
using FactorMaker.Services.Base;
using FactorMaker.Services.ServiceIntefaces;
using Mapster;
using Models;
using Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Factor;
using ViewModels.Store;

namespace FactorMaker.Services
{
    public class FactorService : BaseServiceWithDatabase, IFactorService
    {
        public FactorService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<Result<FactorViewModel>> InsertAsync(FactorViewModel viewModel)
        {
            try
            {
                var result = new Result<FactorViewModel>();
                result.IsSuccessful = true;

                Customer owner = await UnitOfWork.CustomerRepository.GetByIdAsync(viewModel.OwnerId);
                if (owner == null)
                {
                    result.AddErrorMessage(typeof(Customer) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                Store store = await UnitOfWork.StoreRepository.GetByIdAsync(viewModel.StoreId);
                if (store == null)
                {
                    result.AddErrorMessage(typeof(Store) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                var factor = viewModel.Adapt<Factor>();
                factor.Owner = null;
                factor.Store = null;
                factor.TotalPrice = 0;
                if (factor.SellDateTime == default)
                {
                    factor.SellDateTime = DateTime.Now;
                }

                await UnitOfWork.FactorRepository.InsertAsync(factor);
                await UnitOfWork.SaveAsync();

                result.Data = factor.Adapt<FactorViewModel>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Result<FactorViewModel>> UpdateAsync(FactorViewModel viewModel)
        {
            try
            {
                var result = new Result<FactorViewModel>();
                result.IsSuccessful = true;

                var factor = await UnitOfWork.FactorRepository.GetWithItemsByIdAsync(viewModel.Id);
                if (factor == null)
                {
                    result.AddErrorMessage(typeof(Factor) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                var owner = await UnitOfWork.CustomerRepository.GetByIdAsync(viewModel.OwnerId);
                if (owner == null)
                {
                    result.AddErrorMessage(nameof(owner) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                var store = await UnitOfWork.StoreRepository.GetByIdAsync(viewModel.StoreId);
                if (store == null)
                {
                    result.AddErrorMessage(typeof(Store) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                factor.Description = viewModel.Description;
                factor.OwnerId = viewModel.OwnerId;
                factor.StoreId = viewModel.StoreId;
                factor.IsClosed = viewModel.IsClosed;
                factor.SellDateTime = viewModel.SellDateTime == default ? factor.SellDateTime : viewModel.SellDateTime;
                factor.TotalPrice = CalculateFactorTotalPrice(factor);

                await UnitOfWork.FactorRepository.UpdateAsync(factor);
                await UnitOfWork.SaveAsync();

                result.IsSuccessful = true;
                result.Data = factor.Adapt<FactorViewModel>();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Result> DeleteByIdAsync(Guid id)
        {
            try
            {
                var result = new Result();
                result.IsSuccessful = true;

                Factor factor = await UnitOfWork.FactorRepository.GetByIdAsync(id);
                if (factor == null)
                {
                    result.AddErrorMessage(typeof(Factor) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                await UnitOfWork.FactorRepository.DeleteAsync(factor);
                await UnitOfWork.SaveAsync();

                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Result<FactorViewModel>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = new Result<FactorViewModel>();
                result.IsSuccessful = true;

                Factor factor = await UnitOfWork.FactorRepository.GetByIdAsync(id);
                if (factor == null)
                {
                    result.AddErrorMessage(typeof(Factor) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                result.Data = factor.Adapt<FactorViewModel>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Result<ICollection<FactorViewModel>>> GetByOwnerIdAsync(Guid ownerId)
        {
            try
            {
                var result = new Result<ICollection<FactorViewModel>>();
                result.IsSuccessful = true;

                var factors = await UnitOfWork.FactorRepository.GetByOwnerIdAsync(ownerId);
                result.Data = factors.Adapt<ICollection<FactorViewModel>>();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Result<TotalFactorViewModel>> CalculateFactorByIdAsync(Guid id)
        {
            var result = new Result<TotalFactorViewModel>();
            result.IsSuccessful = true;

            var factor = await UnitOfWork.FactorRepository.GetWithItemsByIdAsync(id);
            if (factor == null)
            {
                result.AddErrorMessage(typeof(Factor) + " " + ErrorMessages.NotFound);
                result.IsSuccessful = false;
                return result;
            }

            var store = await UnitOfWork.StoreRepository.GetByIdAsync(factor.StoreId);
            if (store == null)
            {
                result.AddErrorMessage(typeof(Store) + " " + ErrorMessages.NotFound);
                result.IsSuccessful = false;
                return result;
            }

            TotalFactorViewModel totalFactor = new TotalFactorViewModel()
            {
                Store = store.Adapt<StoreViewModel>(),
                Id = factor.Id,
                InsertDateTime = factor.InsertDateTime,
                OwnerFullName = factor.Owner.FullName,
                OwnerId = factor.Owner.Id,
                TotalPrice = 0
            };

            foreach (var item in factor.FactorItems.Where(x => x.IsDeleted == false))
            {
                var totalItem = new ViewModels.FactorItem.TotalFactorItemViewModel()
                {
                    Offpercent = item.OffPercent,
                    ProductName = item.Product.Name,
                    Quantity = item.Quantity
                };

                totalFactor.TotalPrice += CalculateLineTotal(item.Price, item.Quantity, item.OffPercent);
                totalFactor.FatorItems.Add(totalItem);
            }

            result.Data = totalFactor;
            return result;
        }

        public async Task<Result<FactorViewModel>> GetFactorWithItemsByIdAsync(User user, Guid id)
        {
            try
            {
                var result = new Result<FactorViewModel>();
                result.IsSuccessful = true;

                var factor = await UnitOfWork.FactorRepository.GetWithItemsByIdAsync(id);
                if (factor == null)
                {
                    result.AddErrorMessage(typeof(Factor) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }
                else if (await HasAccessUserToStore(user, factor.StoreId) == false)
                {
                    result.AddErrorMessage(ErrorMessages.UnauthorizedAccess);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                if (factor.FactorItems != null)
                {
                    factor.FactorItems = factor.FactorItems.Where(fi => fi.IsDeleted == false).ToList();
                }

                factor.TotalPrice = CalculateFactorTotalPrice(factor);
                result.Data = factor.Adapt<FactorViewModel>();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Result<ICollection<FactorViewModel>>> GetByStoreIdAsync(User user, Guid storeId)
        {
            try
            {
                var result = new Result<ICollection<FactorViewModel>>();
                result.IsSuccessful = true;

                if (await HasAccessUserToStore(user, storeId) == false)
                {
                    result.AddErrorMessage(ErrorMessages.UnauthorizedAccess);
                    result.IsSuccessful = false;
                    return result;
                }

                var store = await UnitOfWork.StoreRepository.GetByIdAsync(storeId);
                if (store == null)
                {
                    result.AddErrorMessage(typeof(Store) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                    return result;
                }

                var factors = await UnitOfWork.FactorRepository.GetByStoreIdAsync(storeId);
                result.Data = factors.Adapt<ICollection<FactorViewModel>>();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Result<ICollection<FactorSaleMonthlyViewModel>>> GetMonthlyFactorSaleAsync(User user, int year, Guid storeId)
        {
            try
            {
                var result = new Result<ICollection<FactorSaleMonthlyViewModel>>();
                result.IsSuccessful = true;

                if (await HasAccessUserToStore(user, storeId) == false)
                {
                    result.AddErrorMessage(ErrorMessages.UnauthorizedAccess);
                    result.IsSuccessful = false;
                    return result;
                }

                var store = await UnitOfWork.StoreRepository.GetByIdAsync(storeId);
                if (store == null)
                {
                    result.AddErrorMessage(typeof(Store) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                    return result;
                }

                PersianCalendar pcal = new PersianCalendar();
                var startDayPersianYear = new DateTime(year, 1, 1, pcal);
                var endDayPersianYear = pcal.IsLeapYear(year)
                    ? new DateTime(year, 12, 30, 23, 59, 59, pcal)
                    : new DateTime(year, 12, 29, 23, 59, 59, pcal);

                var monthlySaleDtos = await UnitOfWork.FactorRepository.GetMonthlyFactorSaleAsync(startDayPersianYear, endDayPersianYear, storeId);

                result.Data = monthlySaleDtos.Select(dto => new FactorSaleMonthlyViewModel
                {
                    Count = dto.Count,
                    PersianMonth = dto.Month.ToString(),
                    PersianYear = dto.Year,
                    TotalPrice = dto.TotalPrice
                }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Result<ICollection<FactorSaleWeeklyViewModel>>> GetWeeklyFactorSaleAsync(User user, int year, int month, Guid storeId)
        {
            try
            {
                var result = new Result<ICollection<FactorSaleWeeklyViewModel>>();
                result.IsSuccessful = true;

                if (await HasAccessUserToStore(user, storeId) == false)
                {
                    result.AddErrorMessage(ErrorMessages.UnauthorizedAccess);
                    result.IsSuccessful = false;
                    return result;
                }

                var store = await UnitOfWork.StoreRepository.GetByIdAsync(storeId);
                if (store == null)
                {
                    result.AddErrorMessage(typeof(Store) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                    return result;
                }

                PersianCalendar pcal = new PersianCalendar();
                var startDay = new DateTime(year, month, 1, pcal);
                var daysInMonth = month <= 6 ? 31 : (month < 12 ? 30 : (pcal.IsLeapYear(year) ? 30 : 29));
                var endDay = new DateTime(year, month, daysInMonth, 23, 59, 59, pcal);

                var monthlySalesDto = await UnitOfWork.FactorRepository.GetWeeklyFactorSaleAsync(startDay, endDay, storeId);

                result.Data = monthlySalesDto.Select(dto => new FactorSaleWeeklyViewModel
                {
                    Count = dto.Count,
                    Week = dto.Week,
                    Year = dto.Year,
                    TotalPrice = dto.TotalPrice
                }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Result<ICollection<FactorSaleHourlyViewModel>>> GetHourlyFactorSaleAsync(User user, DateTime dtFrom, DateTime dtTo, Guid storeId)
        {
            try
            {
                var result = new Result<ICollection<FactorSaleHourlyViewModel>>();
                result.IsSuccessful = true;

                if (await HasAccessUserToStore(user, storeId) == false)
                {
                    result.AddErrorMessage(ErrorMessages.UnauthorizedAccess);
                    result.IsSuccessful = false;
                    return result;
                }

                var store = await UnitOfWork.StoreRepository.GetByIdAsync(storeId);
                if (store == null)
                {
                    result.AddErrorMessage(typeof(Store) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                    return result;
                }

                var hourlySaleDtos = await UnitOfWork.FactorRepository.GetHourlyFactorSaleAsync(dtFrom, dtTo, storeId);

                result.Data = hourlySaleDtos.Select(dto => new FactorSaleHourlyViewModel
                {
                    Count = dto.Count,
                    Hour = dto.Hour,
                    TotalPrice = dto.TotalPrice
                }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Result<ICollection<FactorSaleWeekDailyViewModel>>> GetWeekDailyFactorSaleAsync(User user, DateTime dtFrom, DateTime dtTo, Guid storeId)
        {
            try
            {
                var result = new Result<ICollection<FactorSaleWeekDailyViewModel>>();
                result.IsSuccessful = true;

                if (await HasAccessUserToStore(user, storeId) == false)
                {
                    result.AddErrorMessage(ErrorMessages.UnauthorizedAccess);
                    result.IsSuccessful = false;
                    return result;
                }

                var store = await UnitOfWork.StoreRepository.GetByIdAsync(storeId);
                if (store == null)
                {
                    result.AddErrorMessage(typeof(Store) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                    return result;
                }

                var hourlySaleDtos = await UnitOfWork.FactorRepository.GetWeekDailyFactorSaleAsync(dtFrom, dtTo, storeId);

                result.Data = hourlySaleDtos.Select(dto => new FactorSaleWeekDailyViewModel
                {
                    DayOfWeek = dto.DayOfWeek,
                    Count = dto.Count,
                    TotalPrice = dto.TotalPrice
                }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private decimal CalculateFactorTotalPrice(Factor factor)
        {
            if (factor?.FactorItems == null)
            {
                return factor?.TotalPrice ?? 0;
            }

            return factor.FactorItems
                .Where(item => item.IsDeleted == false)
                .Sum(item => CalculateLineTotal(item.Price, item.Quantity, item.OffPercent));
        }

        private decimal CalculateLineTotal(decimal price, int quantity, int offPercent)
        {
            var discountRatio = Math.Min(100, Math.Max(0, offPercent)) / 100m;
            return price * quantity * (1 - discountRatio);
        }
    }
}


