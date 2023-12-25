using Common;
using Data;
using FactorMaker.Services.Base;
using FactorMaker.Services.ServicesIntefaces;
using Mapster;
using Models;
using Resources;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Factor;

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

                User creator = await UnitOfWork.UserRepository.GetByIdAsync(viewModel.CreatorId);
                if (creator == null)
                {
                    result.AddErrorMessage(typeof(User) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                var factor = viewModel.Adapt<Factor>();

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

                var factor = await UnitOfWork.FactorRepository.GetByIdAsync(viewModel.Id);
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

                if (result.IsSuccessful == false) return result;

                factor.Description = viewModel.Description;

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

            Factor factor = await UnitOfWork.FactorRepository.GetWithItemsByIdAsync(id);
            if (factor == null)
            {
                result.AddErrorMessage(typeof(Factor) + " " + ErrorMessages.NotFound);
                result.IsSuccessful = false;
            }

            if (result.IsSuccessful == false) return result;


            TotalFactorViewModel totalFactor = new TotalFactorViewModel()
            {
                CreatorFullName = factor.Creator.FullName,
                CreatorId = factor.Creator.Id,
                Id = factor.Id,
                InsertDateTime = factor.InsertDateTime,
                OwnerFullName = factor.Owner.FullName,
                OwnerId = factor.Owner.Id,
                TotalPrice = 0
            };

            foreach (var item in factor.FactorItems)
            {

                var totalItem = new ViewModels.FactorItem.TotalFactorItemViewModel()
                {
                    Offpercent = item.OffPercent,
                    ProductName = item.Product.Name,
                    Quantity = item.Quantity
                };

                totalFactor.TotalPrice += (item.Product.Price - (item.Product.Price * (item.OffPercent / 100)) * item.Quantity);
                totalFactor.FatorItems.Add(totalItem);
            }

            result.Data = totalFactor;

            return result;
        }
        public async Task<Result<FactorViewModel>> GetFactorWithItemsByIdAsync(Guid id)
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

                if (result.IsSuccessful == false) return result;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<ICollection<FactorViewModel>>> GetByStoreIdAsync(Guid storeId)
        {
            try
            {
                var result = new Result<ICollection<FactorViewModel>>();
                result.IsSuccessful = true;

                var factors = await UnitOfWork.FactorRepository.GetByStoreIdAsync(storeId);

                result.Data = factors.Adapt<ICollection<FactorViewModel>>();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
