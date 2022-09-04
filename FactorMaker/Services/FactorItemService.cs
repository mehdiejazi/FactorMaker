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
    public class FactorItemService : BaseServiceWithDatabase, IFactorItemService
    {
        public FactorItemService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public FactorItem Update(Guid id, Guid productId, int quantity, byte offPercent)
        {
            try
            {
                FactorItem factorItem = UnitOfWork.FactorItemRepository.GetById(id);
                if (factorItem == null)
                    throw new NullReferenceException(typeof(FactorItem) + " " + ErrorMessages.NotFound);

                Product product = UnitOfWork.ProductRepository.GetById(productId);
                if (product == null)
                    throw new NullReferenceException(typeof(Product) + " " + ErrorMessages.NotFound);

                factorItem.Product = product;
                factorItem.Quantity = quantity;
                factorItem.OffPercent = offPercent;

                UnitOfWork.FactorItemRepository.Update(factorItem);
                UnitOfWork.Save();

                return factorItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<FactorItem> UpdateAsync(Guid id, Guid productId, int quantity, byte offPercent)
        {
            try
            {
                FactorItem factorItem = await UnitOfWork.FactorItemRepository.GetByIdAsync(id);
                if (factorItem == null)
                    throw new NullReferenceException(typeof(FactorItem) + " " + ErrorMessages.NotFound);

                Product product = await UnitOfWork.ProductRepository.GetByIdAsync(productId);
                if (product == null)
                    throw new NullReferenceException(typeof(Product) + " " + ErrorMessages.NotFound);

                factorItem.Product = product;
                factorItem.Quantity = quantity;
                factorItem.OffPercent = offPercent;

                await UnitOfWork.FactorItemRepository.UpdateAsync(factorItem);
                await UnitOfWork.SaveAsync();

                return factorItem;
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
                FactorItem factorItem = UnitOfWork.FactorItemRepository.GetById(id);

                if (factorItem == null)
                    throw new NullReferenceException(typeof(User) + " " + ErrorMessages.NotFound);

                UnitOfWork.FactorItemRepository.Delete(factorItem);
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
                FactorItem factorItem = await UnitOfWork.FactorItemRepository.GetByIdAsync(id);

                if (factorItem == null)
                    throw new NullReferenceException(typeof(User) + " " + ErrorMessages.NotFound);

                await UnitOfWork.FactorItemRepository.DeleteAsync(factorItem);
                await UnitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public FactorItem GetById(Guid id)
        {
            try
            {
                FactorItem factorItem = UnitOfWork.FactorItemRepository.GetById(id);

                if (factorItem == null)
                    throw new NullReferenceException(typeof(Factor) + " " + ErrorMessages.NotFound);

                return factorItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<FactorItem> GetByIdAsync(Guid id)
        {
            try
            {
                FactorItem factorItem = await UnitOfWork.FactorItemRepository.GetByIdAsync(id);

                if (factorItem == null)
                    throw new NullReferenceException(typeof(Factor) + " " + ErrorMessages.NotFound);

                return factorItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
