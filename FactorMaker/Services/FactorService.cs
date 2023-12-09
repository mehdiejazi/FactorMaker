using FactorMaker.Services.Base;
using Data;
using Models;
using System;
using System.Threading.Tasks;
using Resources;
using System.Collections.Generic;
using FactorMaker.Services.ServicesIntefaces;
using System.Linq;
using ViewModels.Factor;

namespace FactorMaker.Services
{
    public class FactorService : BaseServiceWithDatabase, IFactorService
    {
        public FactorService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Factor Insert(Guid ownerId, Guid creatorId)
        {
            try
            {
                Customer owner = UnitOfWork.CustomerRepository.GetById(ownerId);

                if (owner == null)
                    throw new NullReferenceException(typeof(Customer) + " " + ErrorMessages.NotFound);

                User creator = UnitOfWork.UserRepository.GetById(ownerId);

                if (creator == null)
                    throw new NullReferenceException(typeof(User) + " " + ErrorMessages.NotFound);

                Factor factor = new Factor
                {
                    Owner = owner,
                    Creator = creator
                };

                UnitOfWork.FactorRepository.Insert(factor);
                UnitOfWork.Save();

                return factor;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Factor> InsertAsync(Guid ownerId, Guid creatorId)
        {
            try
            {
                Customer owner = await UnitOfWork.CustomerRepository.GetByIdAsync(ownerId);

                if (owner == null)
                    throw new NullReferenceException(typeof(Customer) + " " + ErrorMessages.NotFound);

                User creator = await UnitOfWork.UserRepository.GetByIdAsync(ownerId);

                if (creator == null)
                    throw new NullReferenceException(typeof(User) + " " + ErrorMessages.NotFound);

                Factor factor = new Factor
                {
                    Owner = owner,
                    Creator = creator
                };

                await UnitOfWork.FactorRepository.InsertAsync(factor);
                await UnitOfWork.SaveAsync();

                return factor;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public FactorItem InsertFactorItem(Guid factorId, Guid productId, int quantity, byte offpercent)
        {
            try
            {
                Factor factor = UnitOfWork.FactorRepository.GetById(factorId);
                if (factor == null)
                    throw new NullReferenceException(typeof(Factor) + " " + ErrorMessages.NotFound);

                Product product = UnitOfWork.ProductRepository.GetById(productId);
                if (product == null)
                    throw new NullReferenceException(typeof(Product) + " " + ErrorMessages.NotFound);

                FactorItem factorItem = new FactorItem()
                {
                    Product = product,
                    Quantity = quantity,
                    OffPercent = offpercent
                };

                factor.FactorItems.Add(factorItem);
                UnitOfWork.Save();

                return factorItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<FactorItem> InsertFactorItemAsync(Guid factorId, Guid productId, int quantity, byte offpercent)
        {
            try
            {
                Factor factor = await UnitOfWork.FactorRepository.GetByIdAsync(factorId);
                if (factor == null)
                    throw new NullReferenceException(typeof(Factor) + " " + ErrorMessages.NotFound);

                Product product = await UnitOfWork.ProductRepository.GetByIdAsync(productId);
                if (product == null)
                    throw new NullReferenceException(typeof(Product) + " " + ErrorMessages.NotFound);

                FactorItem factorItem = new FactorItem()
                {
                    Product = product,
                    Quantity = quantity,
                    OffPercent = offpercent
                };

                factor.FactorItems.Add(factorItem);
                await UnitOfWork.SaveAsync();

                return factorItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Factor Update(Guid id, Guid ownerId)
        {
            try
            {
                Factor factor = UnitOfWork.FactorRepository.GetById(id);
                if (factor == null)
                    throw new NullReferenceException(typeof(Factor) + " " + ErrorMessages.NotFound);


                Customer owner = UnitOfWork.CustomerRepository.GetById(ownerId);
                if (owner == null)
                    throw new NullReferenceException(typeof(Customer) + " " + ErrorMessages.NotFound);

                factor.Owner = owner;

                UnitOfWork.FactorRepository.Update(factor);
                UnitOfWork.Save();

                return factor;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Factor> UpdateAsync(Guid id, Guid ownerId)
        {
            try
            {
                Factor factor = await UnitOfWork.FactorRepository.GetByIdAsync(id);
                if (factor == null)
                    throw new NullReferenceException(typeof(Factor) + " " + ErrorMessages.NotFound);


                Customer owner = await UnitOfWork.CustomerRepository.GetByIdAsync(ownerId);
                if (owner == null)
                    throw new NullReferenceException(typeof(Customer) + " " + ErrorMessages.NotFound);

                factor.Owner = owner;

                await UnitOfWork.FactorRepository.UpdateAsync(factor);
                await UnitOfWork.SaveAsync();

                return factor;
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
                Factor factor = UnitOfWork.FactorRepository.GetById(id);

                if (factor == null)
                    throw new NullReferenceException(typeof(User) + " " + ErrorMessages.NotFound);

                UnitOfWork.FactorRepository.Delete(factor);
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
                Factor factor = await UnitOfWork.FactorRepository.GetByIdAsync(id);

                if (factor == null)
                    throw new NullReferenceException(typeof(User) + " " + ErrorMessages.NotFound);

                await UnitOfWork.FactorRepository.DeleteAsync(factor);
                await UnitOfWork.SaveAsync();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ICollection<FactorItem> GetFactorItemsById(Guid id)
        {
            try
            {
                Factor factor = UnitOfWork.FactorRepository.GetById(id);

                if (factor == null)
                    throw new NullReferenceException(typeof(Factor) + " " + ErrorMessages.NotFound);

                return factor.FactorItems;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ICollection<FactorItem>> GetFactorItemsByIdAsync(Guid id)
        {
            try
            {
                Factor factor = await UnitOfWork.FactorRepository.GetByIdAsync(id);

                if (factor == null)
                    throw new NullReferenceException(typeof(Factor) + " " + ErrorMessages.NotFound);

                return factor.FactorItems;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Factor GetById(Guid id)
        {
            try
            {
                Factor factor = UnitOfWork.FactorRepository.GetById(id);

                if (factor == null)
                    throw new NullReferenceException(typeof(Factor) + " " + ErrorMessages.NotFound);

                return factor;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Factor> GetByIdAsync(Guid id)
        {
            try
            {
                Factor factor = await UnitOfWork.FactorRepository.GetByIdAsync(id);

                if (factor == null)
                    throw new NullReferenceException(typeof(Factor) + " " + ErrorMessages.NotFound);

                return factor;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ICollection<Factor> GetAll()
        {
            try
            {
                var factors = UnitOfWork.FactorRepository.GetAll();

                return factors;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ICollection<Factor>> GetAllAsync()
        {
            try
            {
                var factors = await UnitOfWork.FactorRepository.GetAllAsync();

                return factors;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public TotalFactorViewModel CalculateFactorById(Guid id)
        {
            try
            {
                Factor factor = UnitOfWork.FactorRepository.GetFactorWithItemsById(id);

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
                    
                    var totalItem = new TotalFactorItemViewModel()
                    {
                        Offpercent = item.OffPercent,
                        ProductName = item.Product.Name,
                        Quantity = item.Quantity
                    };

                    totalFactor.TotalPrice += item.Product.Price * (item.OffPercent / 100) * item.Quantity;
                    totalFactor.FatorItems.Add(totalItem);
                }

                return totalFactor;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<TotalFactorViewModel> CalculateFactorByIdAsync(Guid id)
        {
            Factor factor = await UnitOfWork.FactorRepository.GetFactorWithItemsByIdAsync(id);

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

                var totalItem = new TotalFactorItemViewModel()
                {
                    Offpercent = item.OffPercent,
                    ProductName = item.Product.Name,
                    Quantity = item.Quantity
                };

                totalFactor.TotalPrice += item.Product.Price * (item.OffPercent / 100) * item.Quantity;
                totalFactor.FatorItems.Add(totalItem);
            }

            return totalFactor;
        }

        public Factor GetFactorWithItemsById(Guid id)
        {
            try
            {
                var factor = UnitOfWork.FactorRepository.GetFactorWithItemsById(id);

                return factor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Factor> GetFactorWithItemsByIdAsync(Guid id)
        {
            try
            {
                var factor = await UnitOfWork.FactorRepository.GetFactorWithItemsByIdAsync(id);

                return factor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
