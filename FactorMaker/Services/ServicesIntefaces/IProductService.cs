using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FactorMaker.Services.ServicesIntefaces
{
    public interface IProductService
    {
        void DeleteById(Guid id);
        Task DeleteByIdAsync(Guid id);
        ICollection<Product> GetAll();
        Task<ICollection<Product>> GetAllAsync();
        Product GetById(Guid id);
        Task<Product> GetByIdAsync(Guid id);
        Product Insert(string name, long price);
        Task<Product> InsertAsync(string name, long price);
        Product Update(Guid id, string name, long price);
        Task<Product> UpdateAsync(Guid id, string name, long price);
    }
}