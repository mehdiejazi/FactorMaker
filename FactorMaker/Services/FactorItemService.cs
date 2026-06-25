using Common;
using Data;
using FactorMaker.Services.Base;
using FactorMaker.Services.ServiceIntefaces;
using Mapster;
using Models;
using Resources;
using System;
using System.Threading.Tasks;
using ViewModels.FactorItem;

namespace FactorMaker.Services
{
    public class FactorItemService : BaseServiceWithDatabase, IFactorItemService
    {
        public FactorItemService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<Result<FactorItemViewModel>> InsertAsync(FactorItemViewModel viewModel)
        {
            try
            {
                var result = new Result<FactorItemViewModel>();
                result.IsSuccessful = true;

                var product = await UnitOfWork.ProductRepository.GetByIdAsync(viewModel.ProductId);
                if (product == null)
                {
                    result.AddErrorMessage(typeof(Product) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                var factor = await UnitOfWork.FactorRepository.GetWithItemsByIdAsync(viewModel.FactorId);
                if (factor == null)
                {
                    result.AddErrorMessage(typeof(Factor) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                var factorItem = viewModel.Adapt<FactorItem>();
                factorItem.Product = null;
                factorItem.Price = product.Price;

                await UnitOfWork.FactorItemRepository.InsertAsync(factorItem);
                await UnitOfWork.SaveAsync();

                factor.TotalPrice = await RecalculateFactorTotalPriceAsync(factor.Id);
                await UnitOfWork.FactorRepository.UpdateAsync(factor);
                await UnitOfWork.SaveAsync();

                result.Data = factorItem.Adapt<FactorItemViewModel>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Result<FactorItemViewModel>> UpdateAsync(FactorItemViewModel viewModel)
        {
            try
            {
                var result = new Result<FactorItemViewModel>();
                result.IsSuccessful = true;

                var factorItem = await UnitOfWork.FactorItemRepository.GetByIdAsync(viewModel.Id);
                if (factorItem == null)
                {
                    result.AddErrorMessage(typeof(FactorItem) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                var factor = await UnitOfWork.FactorRepository.GetWithItemsByIdAsync(viewModel.FactorId);
                if (factor == null)
                {
                    result.AddErrorMessage(typeof(Factor) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                var product = await UnitOfWork.ProductRepository.GetByIdAsync(viewModel.ProductId);
                if (product == null)
                {
                    result.AddErrorMessage(typeof(Product) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                factorItem.OffPercent = viewModel.OffPercent;
                factorItem.Quantity = viewModel.Quantity;
                factorItem.ProductId = viewModel.ProductId;
                factorItem.Price = product.Price;
                factorItem.Description = viewModel.Description;

                await UnitOfWork.FactorItemRepository.UpdateAsync(factorItem);
                await UnitOfWork.SaveAsync();

                factor.TotalPrice = await RecalculateFactorTotalPriceAsync(factor.Id);
                await UnitOfWork.FactorRepository.UpdateAsync(factor);
                await UnitOfWork.SaveAsync();

                result.Data = factorItem.Adapt<FactorItemViewModel>();
                result.IsSuccessful = true;

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

                FactorItem factorItem = await UnitOfWork.FactorItemRepository.GetByIdAsync(id);
                if (factorItem == null)
                {
                    result.AddErrorMessage(typeof(FactorItem) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                var factor = await UnitOfWork.FactorRepository.GetWithItemsByIdAsync(factorItem.FactorId);
                await UnitOfWork.FactorItemRepository.DeleteAsync(factorItem);
                await UnitOfWork.SaveAsync();

                if (factor != null)
                {
                    factor.TotalPrice = await RecalculateFactorTotalPriceAsync(factor.Id);
                    await UnitOfWork.FactorRepository.UpdateAsync(factor);
                    await UnitOfWork.SaveAsync();
                }

                result.IsSuccessful = true;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Result<FactorItemViewModel>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = new Result<FactorItemViewModel>();
                result.IsSuccessful = true;

                FactorItem factorItem = await UnitOfWork.FactorItemRepository.GetByIdAsync(id);
                if (factorItem == null)
                {
                    result.AddErrorMessage(typeof(FactorItem) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                    return result;
                }

                result.Data = factorItem.Adapt<FactorItemViewModel>();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<decimal> RecalculateFactorTotalPriceAsync(Guid factorId)
        {
            var factor = await UnitOfWork.FactorRepository.GetWithItemsByIdAsync(factorId);
            if (factor?.FactorItems == null)
            {
                return 0;
            }

            decimal totalPrice = 0;
            foreach (var item in factor.FactorItems)
            {
                if (item.IsDeleted)
                {
                    continue;
                }

                totalPrice += CalculateLineTotal(item.Price, item.Quantity, item.OffPercent);
            }

            return totalPrice;
        }

        private decimal CalculateLineTotal(decimal price, int quantity, int offPercent)
        {
            var discountRatio = Math.Min(100, Math.Max(0, offPercent)) / 100m;
            return price * quantity * (1 - discountRatio);
        }
    }
}
