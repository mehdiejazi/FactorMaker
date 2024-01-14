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
using ViewModels.Product;

namespace FactorMaker.Services
{
    public class ProductService : BaseServiceWithDatabase, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public async Task<Result<ProductViewModel>> InsertAsync(ProductViewModel viewModel)
        {
            try
            {
                var result = new Result<ProductViewModel>();

                User owner = await UnitOfWork.UserRepository.GetByIdAsync(viewModel.OwnerId);
                if (owner == null)
                {
                    result.AddErrorMessage(nameof(viewModel.Owner) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                Category category = await UnitOfWork.CategoryRepository.GetByIdAsync(viewModel.CategoryId);
                if (category == null)
                {
                    result.AddErrorMessage(nameof(Category) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                Product product = viewModel.Adapt<Product>();

                await UnitOfWork.ProductRepository.InsertAsync(product);
                await UnitOfWork.SaveAsync();

                result.Data = product.Adapt<ProductViewModel>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Result<ProductViewModel>> UpdateAsync(ProductViewModel viewModel)
        {
            try
            {
                var result = new Result<ProductViewModel>();

                User owner = await UnitOfWork.UserRepository.GetByIdAsync(viewModel.OwnerId);
                if (owner == null)
                {
                    result.AddErrorMessage(nameof(viewModel.Owner) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                Category category = await UnitOfWork.CategoryRepository.GetByIdAsync(viewModel.CategoryId);
                if (category == null)
                {
                    result.AddErrorMessage(nameof(Category) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                Product product = UnitOfWork.ProductRepository.GetById(viewModel.Id);
                if (product == null)
                    throw new NullReferenceException(typeof(Product) + " " + ErrorMessages.NotFound);

                product.IsDeleted = viewModel.IsDeleted;
                product.Name = viewModel.Name;
                product.Price = viewModel.Price;
                
                await UnitOfWork.ProductRepository.UpdateAsync(product);
                await UnitOfWork.SaveAsync();

                result.Data = product.Adapt<ProductViewModel>();
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

                Product product = await UnitOfWork.ProductRepository.GetByIdAsync(id);

                if (product == null)
                    throw new NullReferenceException(typeof(Product) + " " + ErrorMessages.NotFound);

                await UnitOfWork.ProductRepository.DeleteAsync(product);
                await UnitOfWork.SaveAsync();

                result.IsSuccessful = true;
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Result<ProductViewModel>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = new Result<ProductViewModel>();

                Product product = await UnitOfWork.ProductRepository.GetByIdAsync(id);

                if (product == null)
                    throw new NullReferenceException(typeof(Product) + " " + ErrorMessages.NotFound);

                result.Data = product.Adapt<ProductViewModel>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Result<ICollection<ProductViewModel>>> GetAllAsync()
        {
            try
            {
                var result = new Result<ICollection<ProductViewModel>>();

                var list = await UnitOfWork.ProductRepository.GetAllAsync();

                result.Data = list.Adapt<ICollection<ProductViewModel>>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Result<ICollection<ProductViewModel>>> GetByOwnerIdCategoryIdAsync(Guid ownerId, Guid categoryId)
        {
            try
            {
                var result = new Result<ICollection<ProductViewModel>>();

                var list = await UnitOfWork.ProductRepository.GetByOwnerIdCategoryIdAsync(ownerId, categoryId);

                result.Data = list.Adapt<ICollection<ProductViewModel>>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
       
    }
}
