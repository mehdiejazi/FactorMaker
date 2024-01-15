using Common;
using Data;
using Data.DataTransferObjects.Product;
using FactorMaker.Services.Base;
using FactorMaker.Services.ServicesIntefaces;
using Mapster;
using Models;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Customer;
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
        public async Task<Result<ICollection<ProductSaleTotalQuantityViewModel>>> GetTop10SaleByQuantityAsync(Guid storeId)
        {
            try
            {
                var result = new Result<ICollection<ProductSaleTotalQuantityViewModel>>();
                result.IsSuccessful = true;

                var store = await UnitOfWork.StoreRepository.GetByIdAsync(storeId);
                if (store == null)
                {
                    result.AddErrorMessage(typeof(Store) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                    return result;
                }

                var productSaleTotalQuantityDtos = await UnitOfWork.ProductRepository.GetTop10SaleByQuantityAsync(storeId);

                result.Data = productSaleTotalQuantityDtos
                    .Select(x => new ProductSaleTotalQuantityViewModel
                    {
                        Product = x.Product.Adapt<ProductViewModel>(),
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
        public async Task<Result<ICollection<ProductSaleTotalPriceViewModel>>> GetTop10SaleByPriceAsync(Guid storeId)
        {
            try
            {
                var result = new Result<ICollection<ProductSaleTotalPriceViewModel>>();
                result.IsSuccessful = true;

                var store = await UnitOfWork.StoreRepository.GetByIdAsync(storeId);
                if (store == null)
                {
                    result.AddErrorMessage(typeof(Store) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                    return result;
                }

                var productSaleTotalPriceDtos = await UnitOfWork.ProductRepository.GetTop10SaleByPriceAsync(storeId);

                result.Data = productSaleTotalPriceDtos
                    .Select(x => new ProductSaleTotalPriceViewModel
                    {
                        Product = x.Product.Adapt<ProductViewModel>(),
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
      

        public async Task<Result<ICollection<ProductSaleTotalQuantityViewModel>>> GetSaleTotalByQuantityAsync
            (DateTime dtFrom, DateTime dtTo, Guid storeId)
        {
            try
            {
                var result = new Result<ICollection<ProductSaleTotalQuantityViewModel>>();
                result.IsSuccessful = true;

                var store = await UnitOfWork.StoreRepository.GetByIdAsync(storeId);
                if (store == null)
                {
                    result.AddErrorMessage(typeof(Store) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                    return result;
                }

                var productSaleTotalQuantityDtos = await UnitOfWork.ProductRepository
                    .GetSaleTotalByQuantityAsync(dtFrom,dtTo, storeId);

                result.Data = productSaleTotalQuantityDtos
                    .Select(x => new ProductSaleTotalQuantityViewModel
                    {
                        Product = x.Product.Adapt<ProductViewModel>(),
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

        public async Task<Result<ICollection<ProductSaleTotalPriceViewModel>>> GetSaleTotalByPriceAsync
         (DateTime dtFrom, DateTime dtTo, Guid storeId)
        {
            try
            {
                var result = new Result<ICollection<ProductSaleTotalPriceViewModel>>();
                result.IsSuccessful = true;

                var store = await UnitOfWork.StoreRepository.GetByIdAsync(storeId);
                if (store == null)
                {
                    result.AddErrorMessage(typeof(Store) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                    return result;
                }

                var productSaleTotalPriceDtos = await UnitOfWork.ProductRepository
                    .GetSaleTotalByPriceAsync(dtFrom,dtTo, storeId);

                result.Data = productSaleTotalPriceDtos
                    .Select(x => new ProductSaleTotalPriceViewModel
                    {
                        Product = x.Product.Adapt<ProductViewModel>(),
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


    }
}
