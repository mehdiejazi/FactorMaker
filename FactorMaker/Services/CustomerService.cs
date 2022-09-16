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
    public class CustomerService : BaseServiceWithDatabase, ICustomerService
    {
        public CustomerService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Customer Insert(string firstName, string lastName, string nationalCode)
        {
            try
            {
                Customer customer = new Customer
                {
                    FirstName = firstName,
                    LastName = lastName,
                    NationalCode = nationalCode,
                };

                UnitOfWork.CustomerRepository.Insert(customer);
                UnitOfWork.Save();

                return customer;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Customer> InsertAsync(string firstName, string lastName, string nationalCode)
        {
            try
            {
                Customer customer = new Customer
                {
                    FirstName = firstName,
                    LastName = lastName,
                    NationalCode = nationalCode,
                };

                await UnitOfWork.CustomerRepository.InsertAsync(customer);
                await UnitOfWork.SaveAsync();

                return customer;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Customer Update(Guid id, string firstName, string lastName, string nationalCode)
        {
            try
            {
                Customer customer = UnitOfWork.CustomerRepository.GetById(id);

                if (customer == null)
                    throw new NullReferenceException(typeof(Customer) + " " + ErrorMessages.NotFound);

                customer.FirstName = firstName;
                customer.LastName = lastName;
                customer.NationalCode = nationalCode;


                UnitOfWork.CustomerRepository.Update(customer);
                UnitOfWork.Save();

                return customer;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Customer> UpdateAsync(Guid id, string firstName, string lastName, string nationalCode)
        {
            try
            {
                Customer customer = UnitOfWork.CustomerRepository.GetById(id);

                if (customer == null)
                    throw new NullReferenceException(typeof(Customer) + " " + ErrorMessages.NotFound);

                customer.FirstName = firstName;
                customer.LastName = lastName;
                customer.NationalCode = nationalCode;


                await UnitOfWork.CustomerRepository.UpdateAsync(customer);
                await UnitOfWork.SaveAsync();

                return customer;

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
                Customer customer = UnitOfWork.CustomerRepository.GetById(id);

                if (customer == null)
                    throw new NullReferenceException(typeof(Customer) + " " + ErrorMessages.NotFound);

                UnitOfWork.CustomerRepository.Delete(customer);
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
                Customer customer = await UnitOfWork.CustomerRepository.GetByIdAsync(id);

                if (customer == null)
                    throw new NullReferenceException(typeof(Customer) + " " + ErrorMessages.NotFound);

                await UnitOfWork.CustomerRepository.DeleteAsync(customer);
                await UnitOfWork.SaveAsync();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Customer GetById(Guid id)
        {
            try
            {
                Customer customer = UnitOfWork.CustomerRepository.GetById(id);

                if (customer == null)
                    throw new NullReferenceException(typeof(Customer) + " " + ErrorMessages.NotFound);

                return customer;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<Customer> GetByIdAsync(Guid id)
        {
            try
            {
                Customer customer = await UnitOfWork.CustomerRepository.GetByIdAsync(id);

                if (customer == null)
                    throw new NullReferenceException(typeof(Customer) + " " + ErrorMessages.NotFound);

                return customer;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ICollection<Customer> GetAll()
        {
            try
            {
                var customers = UnitOfWork.CustomerRepository.GetAll();

                return customers;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ICollection<Customer>> GetAllAsync()
        {
            try
            {
                var customers = await UnitOfWork.CustomerRepository.GetAllAsync();

                return customers;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
