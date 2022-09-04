using FactorMaker.Services.Base;
using Data;
using Models;
using System;
using System.Threading.Tasks;
using Resources;
using System.Collections.Generic;
using FactorMaker.Services.ServicesIntefaces;

namespace FactorMaker.Services
{
    public class ProductService : BaseServiceWithDatabase, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Product Insert(string name, long price)
        {
            try
            {
                Product product = new Product()
                {
                    Name = name,
                    Price = price
                };
                UnitOfWork.ProductRepository.Insert(product);
                UnitOfWork.Save();

                return product;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Product> InsertAsync(string name, long price)
        {
            try
            {
                Product product = new Product()
                {
                    Name = name,
                    Price = price
                };
                await UnitOfWork.ProductRepository.InsertAsync(product);
                await UnitOfWork.SaveAsync();

                return product;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Product Update(Guid id, string name, long price)
        {
            try
            {
                Product product = UnitOfWork.ProductRepository.GetById(id);

                if (product == null)
                    throw new NullReferenceException(typeof(Product) + " " + ErrorMessages.NotFound);

                product.Name = name;
                product.Price = price;

                UnitOfWork.ProductRepository.Update(product);
                UnitOfWork.Save();

                return product;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Product> UpdateAsync(Guid id, string name, long price)
        {
            try
            {
                Product product = UnitOfWork.ProductRepository.GetById(id);

                if (product == null)
                    throw new NullReferenceException(typeof(Product) + " " + ErrorMessages.NotFound);

                product.Name = name;
                product.Price = price;

                await UnitOfWork.ProductRepository.UpdateAsync(product);
                await UnitOfWork.SaveAsync();

                return product;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DeleteById(Guid id)
        {
            try
            {
                Product product = UnitOfWork.ProductRepository.GetById(id);

                if (product == null)
                    throw new NullReferenceException(typeof(Product) + " " + ErrorMessages.NotFound);

                UnitOfWork.ProductRepository.Delete(product);
                UnitOfWork.Save();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            try
            {
                Product product = UnitOfWork.ProductRepository.GetById(id);

                if (product == null)
                    throw new NullReferenceException(typeof(Product) + " " + ErrorMessages.NotFound);

                await UnitOfWork.ProductRepository.DeleteAsync(product);
                await UnitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Product GetById(Guid id)
        {
            try
            {
                Product product = UnitOfWork.ProductRepository.GetById(id);

                if (product == null)
                    throw new NullReferenceException(typeof(Product) + " " + ErrorMessages.NotFound);

                return product;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<Product> GetByIdAsync(Guid id)
        {
            try
            {
                Product product = await UnitOfWork.ProductRepository.GetByIdAsync(id);

                if (product == null)
                    throw new NullReferenceException(typeof(Product) + " " + ErrorMessages.NotFound);

                return product;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ICollection<Product> GetAll()
        {
            try
            {
                var products = UnitOfWork.ProductRepository.GetAll();

                return products;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ICollection<Product>> GetAllAsync()
        {
            try
            {
                var products = await UnitOfWork.ProductRepository.GetAllAsync();

                return products;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
