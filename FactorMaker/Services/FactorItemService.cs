using Common;
using Data;
using FactorMaker.Services.Base;
using FactorMaker.Services.ServicesIntefaces;
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

                var product = UnitOfWork.ProductRepository.GetByIdAsync(viewModel.ProductId);
                if (product == null)
                {
                    result.AddErrorMessage(typeof(Product) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                var factor = UnitOfWork.FactorRepository.GetByIdAsync(viewModel.FactorId);
                if (factor == null)
                {
                    result.AddErrorMessage(typeof(Factor) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                var factorItem = viewModel.Adapt<FactorItem>();

                await UnitOfWork.FactorItemRepository.InsertAsync(factorItem);
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

                var factor = await UnitOfWork.FactorRepository.GetByIdAsync(viewModel.FactorId);
                if (factor == null)
                {
                    result.AddErrorMessage(typeof(FactorItem) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                var product = await UnitOfWork.ProductRepository.GetByIdAsync(viewModel.ProductId);
                if (product == null)
                {
                    result.AddErrorMessage(typeof(Product) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                factorItem.OffPercent = viewModel.OffPercent;
                factorItem.Quantity = viewModel.Quantity;

                await UnitOfWork.FactorItemRepository.UpdateAsync(factorItem);
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

                FactorItem factorItem = await UnitOfWork.FactorItemRepository.GetByIdAsync(id);
                if (factorItem == null)
                {
                    result.AddErrorMessage(typeof(FactorItem) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                await UnitOfWork.FactorItemRepository.DeleteAsync(factorItem);
                await UnitOfWork.SaveAsync();

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
                }

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
