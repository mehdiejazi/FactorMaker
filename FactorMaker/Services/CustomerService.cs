using Common;
using Data;
using FactorMaker.Services.Base;
using FactorMaker.Services.ServicesIntefaces;
using Mapster;
using Models;
using Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

                var customer = await UnitOfWork.CustomerRepository.GetByIdAsync(viewModel.Id);
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
    }
}
