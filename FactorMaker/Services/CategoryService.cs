using Common;
using Data;
using FactorMaker.Services.Base;
using FactorMaker.Services.ServiceIntefaces;
using Mapster;
using Models;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Category;

namespace FactorMaker.Services
{
    public class CategoryService : BaseServiceWithDatabase, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public async Task<Result<CategoryViewModel>> InsertAsync(CategoryViewModel viewModel)
        {
            try
            {
                var result = new Result<CategoryViewModel>();

                var store = await UnitOfWork.StoreRepository.GetByIdAsync(viewModel.StoreId);
                if (store == null)
                {
                    result.AddErrorMessage(nameof(Store) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;

                    return result;
                }

                var category = viewModel.Adapt<Category>();

                await UnitOfWork.CategoryRepository.InsertAsync(category);
                await UnitOfWork.SaveAsync();

                result.Data = category.Adapt<CategoryViewModel>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<CategoryViewModel>> UpdateAsync(CategoryViewModel viewModel)
        {
            try
            {
                var result = new Result<CategoryViewModel>();
                result.IsSuccessful = true;


                var store = await UnitOfWork.StoreRepository.GetByIdAsync(viewModel.StoreId);
                if (store == null)
                {
                    result.AddErrorMessage(nameof(Store) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;

                    return result;
                }

                Category category = UnitOfWork.CategoryRepository.GetById(viewModel.Id);
                if (category == null)
                {
                    result.AddErrorMessage(typeof(Category) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                category.DeleteDateTime = viewModel.DeleteDateTime;
                category.IsDeleted = viewModel.IsDeleted;
                category.Name = viewModel.Name;
                category.UpdateDateTime = viewModel.UpdateDateTime;

                await UnitOfWork.CategoryRepository.UpdateAsync(category);
                await UnitOfWork.SaveAsync();

                result.Data = category.Adapt<CategoryViewModel>();
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
                Result result = new Result();
                result.IsSuccessful = true;


                var category = UnitOfWork.CategoryRepository.GetById(id);
                if (category == null)
                {
                    result.AddErrorMessage(typeof(Category) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                await UnitOfWork.CategoryRepository.DeleteAsync(category);
                await UnitOfWork.SaveAsync();

                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<CategoryViewModel>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = new Result<CategoryViewModel>();
                result.IsSuccessful = true;

                var category = await UnitOfWork.CategoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    result.AddErrorMessage(typeof(Category) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                result.Data = category.Adapt<CategoryViewModel>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<ICollection<CategoryViewModel>>> GetAllAsync()
        {
            try
            {
                var result = new Result<ICollection<CategoryViewModel>>();

                var categories = await UnitOfWork.CategoryRepository.GetAllAsync();

                result.Data = categories.Adapt<ICollection<CategoryViewModel>>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Result<ICollection<CategoryViewModel>>> GetByStoreIdAsync(Guid storeId)
        {
            try
            {
                var result = new Result<ICollection<CategoryViewModel>>();

                var categories = await UnitOfWork.CategoryRepository.GetByStoreIdAsync(storeId);

                result.Data = categories.Adapt<ICollection<CategoryViewModel>>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Result<ICollection<CategorySaleTotalPriceViewModel>>> GetSaleTotalByPriceAsync
            (User user,DateTime dtFrom, DateTime dtTo, Guid storeId)
        {
            try
            {
                var result = new Result<ICollection<CategorySaleTotalPriceViewModel>>();
                result.IsSuccessful = true;

                var store = await UnitOfWork.StoreRepository.GetByIdAsync(storeId);
                if (store == null)
                {
                    result.AddErrorMessage(typeof(Store) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                    return result;
                }

                var categorySaleTotalPriceDtos = await UnitOfWork.CategoryRepository
                    .GetSaleTotalByPriceAsync(dtFrom, dtTo, storeId);

                result.Data = categorySaleTotalPriceDtos
                    .Select(x => new CategorySaleTotalPriceViewModel
                    {
                        Category = x.Category.Adapt<CategoryViewModel>(),
                        TotalPrice = x.TotalPrice
                    })
                    .ToList();

                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<ICollection<CategorySaleTotalQuantityViewModel>>> GetSaleTotalQuantityAsync
            (User user, DateTime dtFrom, DateTime dtTo, Guid storeId)
        {
            try
            {
                var result = new Result<ICollection<CategorySaleTotalQuantityViewModel>>();
                result.IsSuccessful = true;

                var store = await UnitOfWork.StoreRepository.GetByIdAsync(storeId);
                if (store == null)
                {
                    result.AddErrorMessage(typeof(Store) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                    return result;
                }

                var categorySaleTotalQuantityDto = await UnitOfWork.CategoryRepository
                    .GetSaleTotalByQuantityAsync(dtFrom, dtTo, storeId);

                result.Data = categorySaleTotalQuantityDto
                    .Select(x => new CategorySaleTotalQuantityViewModel
                    {
                        Category = x.Category.Adapt<CategoryViewModel>(),
                        TotalQuantity = x.TotalQuantity
                    })
                    .ToList();

                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
