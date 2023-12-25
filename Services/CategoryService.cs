using Data;
using FactorMaker.Services.Base;
using FactorMaker.Services.ServicesIntefaces;
using Models;
using Resources;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Mapster;
using Common;

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

                var category = viewModel.Adapt<Category>();

                await UnitOfWork.CategoryRepository.InsertAsync(category);
                await UnitOfWork.SaveAsync();

                result.Data = viewModel.Adapt<CategoryViewModel>();
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

                var category = UnitOfWork.CategoryRepository.GetById(id);
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
        public async Task<Result<ICollection<CategoryViewModel>>> GetByOwnerIdAsync(Guid ownerId)
        {
            try
            {
                var result = new Result<ICollection<CategoryViewModel>>();

                var categories = await UnitOfWork.CategoryRepository.GetByOwnerIdAsync(ownerId);

                result.Data = categories.Adapt<ICollection<CategoryViewModel>>();
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
