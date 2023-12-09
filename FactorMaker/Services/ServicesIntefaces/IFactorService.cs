using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Factor;

namespace FactorMaker.Services.ServicesIntefaces
{
    public interface IFactorService
    {
        void DeleteById(Guid id);
        Task DeleteByIdAsync(Guid id);
        ICollection<Factor> GetAll();
        Task<ICollection<Factor>> GetAllAsync();
        Factor GetById(Guid id);
        Task<Factor> GetByIdAsync(Guid id);
        ICollection<FactorItem> GetFactorItemsById(Guid id);
        Task<ICollection<FactorItem>> GetFactorItemsByIdAsync(Guid id);
        Factor Insert(Guid ownerId, Guid creatorId);
        Task<Factor> InsertAsync(Guid ownerId, Guid creatorId);
        FactorItem InsertFactorItem(Guid factorId, Guid productId, int quantity, byte offpercent);
        Task<FactorItem> InsertFactorItemAsync(Guid factorId, Guid productId, int quantity, byte offpercent);
        Factor Update(Guid id, Guid ownerId);
        Task<Factor> UpdateAsync(Guid id, Guid ownerId);
        TotalFactorViewModel CalculateFactorById(Guid id);
        Task<TotalFactorViewModel> CalculateFactorByIdAsync(Guid id);
        Factor GetFactorWithItemsById(Guid id);
        Task<Factor> GetFactorWithItemsByIdAsync(Guid id);
    }
}