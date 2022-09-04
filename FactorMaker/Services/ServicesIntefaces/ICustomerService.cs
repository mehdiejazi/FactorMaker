using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FactorMaker.Services.ServicesIntefaces
{
    public interface ICustomerService
    {
        void DeleteById(Guid id);
        Task DeleteByIdAsync(Guid id);
        ICollection<Customer> GetAll();
        Task<ICollection<Customer>> GetAllAsync();
        Customer GetById(Guid id);
        Task<Customer> GetByIdAsync(Guid id);
        Customer Insert(string firstName, string lastName, string nationalCode);
        Task<Customer> InsertAsync(string firstName, string lastName, string nationalCode);
        Customer Update(Guid id, string firstName, string lastName, string nationalCode);
        Task<Customer> UpdateAsync(Guid id, string firstName, string lastName, string nationalCode);
    }
}