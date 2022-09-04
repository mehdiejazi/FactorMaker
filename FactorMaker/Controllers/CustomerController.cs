using Common;
using FactorMaker.Services.ServicesIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels;

namespace FactorMaker.Controllers
{
    public class CustomerController : BaseApiController
    {
        private CustomerController() : base()
        {

        }
        public CustomerController(ICustomerService customerService)
        {
            CustomerService = customerService;
        }
        private ICustomerService CustomerService { get; }

        [HttpGet("DeleteById")]
        public Result DeleteById(Guid id)
        {
            CustomerService.DeleteById(id);

            var result = new Result();
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("DeleteByIdAsync")]
        public async Task<Result> DeleteByIdAsync(Guid id)
        {
            await CustomerService.DeleteByIdAsync(id);

            var result = new Result();
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("GetAll")]
        public Result<ICollection<Customer>> GetAll()
        {
            var result = new Result<ICollection<Customer>>();
            result.Data = CustomerService.GetAll();
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("GetAllAsync")]
        public async Task<Result<ICollection<Customer>>> GetAllAsync()
        {
            var result = new Result<ICollection<Customer>>();
            result.Data = await CustomerService.GetAllAsync();
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("GetById")]
        public Result<Customer> GetById(Guid id)
        {
            var result = new Result<Customer>();
            result.Data = CustomerService.GetById(id);
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("GetByIdAsync")]
        public async Task<Result<Customer>> GetByIdAsync(Guid id)
        {
            var result = new Result<Customer>();
            result.Data = await CustomerService.GetByIdAsync(id);
            result.IsSuccessful = true;

            return result;
        }

        [HttpPost("Insert")]
        public Result<Customer> Insert(CustomerViewModel customerViewModel)
        {
            var result = new Result<Customer>();
            result.Data = CustomerService.Insert(customerViewModel.FirstName, customerViewModel.LastName,
                customerViewModel.NationalCode);
            result.IsSuccessful = true;

            return result;
        }

        [HttpPost("InsertAsync")]
        public async Task<Result<Customer>> InsertAsync(CustomerViewModel customerViewModel)
        {
            var result = new Result<Customer>();
            result.Data = await CustomerService.InsertAsync(customerViewModel.FirstName, customerViewModel.LastName,
                customerViewModel.NationalCode);
            result.IsSuccessful = true;

            return result;
        }

        [HttpPost("Update")]
        public Result<Customer> Update(CustomerViewModel customerViewModel)
        {
            var result = new Result<Customer>();
            result.Data = CustomerService.Update(customerViewModel.Id, customerViewModel.FirstName,
                customerViewModel.LastName, customerViewModel.NationalCode);
            result.IsSuccessful = true;

            return result;
        }

        [HttpPost("UpdateAsync")]
        public async Task<Result<Customer>> UpdateAsync(CustomerViewModel customerViewModel)
        {
            var result = new Result<Customer>();
            result.Data = await CustomerService.UpdateAsync(customerViewModel.Id, customerViewModel.FirstName,
                customerViewModel.LastName, customerViewModel.NationalCode);
            result.IsSuccessful = true;

            return result;
        }
   
    }
}
