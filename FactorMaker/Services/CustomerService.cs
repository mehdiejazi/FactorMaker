﻿using Common;
using Data;
using FactorMaker.Services.Base;
using FactorMaker.Services.ServiceIntefaces;
using Mapster;
using Models;
using Resources;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Customer;

namespace FactorMaker.Services
{
    public class CustomerService : BaseServiceWithDatabase, ICustomerService
    {
        public CustomerService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public async Task<Result<CustomerViewModel>> InsertAsync(CustomerViewModel viewModel)
        {
            try
            {
                var result = new Result<CustomerViewModel>();

                Customer customer = viewModel.Adapt<Customer>();

                await UnitOfWork.CustomerRepository.InsertAsync(customer);
                await UnitOfWork.SaveAsync();

                result.Data = customer.Adapt<CustomerViewModel>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<CustomerViewModel>> UpdateAsync(CustomerViewModel viewModel)
        {
            try
            {
                var result = new Result<CustomerViewModel>();
                result.IsSuccessful = true;

                var customer = await UnitOfWork.CustomerRepository.GetByIdAsync(viewModel.Id);
                if (customer == null)
                {
                    result.AddErrorMessage(typeof(Customer) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                await UnitOfWork.CustomerRepository.UpdateAsync(customer);
                await UnitOfWork.SaveAsync();

                result.Data = customer.Adapt<CustomerViewModel>();
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

                var customer = await UnitOfWork.CustomerRepository.GetByIdAsync(id);
                if (customer == null)
                {
                    result.AddErrorMessage(typeof(Customer) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                await UnitOfWork.CustomerRepository.DeleteAsync(customer);
                await UnitOfWork.SaveAsync();

                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<CustomerViewModel>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = new Result<CustomerViewModel>();

                result.IsSuccessful = true;

                var customer = await UnitOfWork.CustomerRepository.GetByIdAsync(id);
                if (customer == null)
                {
                    result.AddErrorMessage(typeof(Customer) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                result.IsSuccessful = true;
                result.Data = customer.Adapt<CustomerViewModel>();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<ICollection<CustomerViewModel>>> GetAllAsync()
        {
            try
            {
                var result = new Result<ICollection<CustomerViewModel>>();

                var customers = await UnitOfWork.CustomerRepository.GetAllAsync();

                result.Data = customers.Adapt<ICollection<CustomerViewModel>>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<ICollection<CustomerViewModel>>> GetByStoreIdAsync(User user,Guid storeId)
        {
            try
            {
                var result = new Result<ICollection<CustomerViewModel>>();
                result.IsSuccessful = true;

                if (await HasAccessUserToStore(user, storeId) == false)
                {
                    result.AddErrorMessage(ErrorMessages.UnauthorizedAccess);
                    result.IsSuccessful = false;
                    return result;
                }

                var customers = await UnitOfWork.CustomerRepository.GetByStoreIdAsync(storeId);

                result.Data = customers.Adapt<ICollection<CustomerViewModel>>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<ICollection<CustomerViewModel>>> GetTop10ByQuantityAsync(User user, Guid storeId)
        {
            try
            {
                var result = new Result<ICollection<CustomerViewModel>>();
                result.IsSuccessful = true;

                if (await HasAccessUserToStore(user, storeId) == false)
                {
                    result.AddErrorMessage(ErrorMessages.UnauthorizedAccess);
                    result.IsSuccessful = false;
                    return result;
                }

                var store = await UnitOfWork.StoreRepository.GetByIdAsync(storeId);
                if (store == null)
                {
                    result.AddErrorMessage(typeof(Store) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                    return result;
                }

                var customers = await UnitOfWork.CustomerRepository.GetTop10CustomersByQuantityAsync(storeId);

                result.Data = customers.Adapt<ICollection<CustomerViewModel>>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<ICollection<CustomerViewModel>>> GetTop10ByPriceAsync(User user, Guid storeId)
        {
            try
            {
                var result = new Result<ICollection<CustomerViewModel>>();
                result.IsSuccessful = true;

                if (await HasAccessUserToStore(user, storeId) == false)
                {
                    result.AddErrorMessage(ErrorMessages.UnauthorizedAccess);
                    result.IsSuccessful = false;
                    return result;
                }

                var store = await UnitOfWork.StoreRepository.GetByIdAsync(storeId);
                if (store == null)
                {
                    result.AddErrorMessage(typeof(Store) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                    return result;
                }

                var customers = await UnitOfWork.CustomerRepository.GetTop10CustomersByQuantityAsync(storeId);

                result.Data = customers.Adapt<ICollection<CustomerViewModel>>();
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
