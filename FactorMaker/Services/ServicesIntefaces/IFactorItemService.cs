using Models;
using System;
using System.Threading.Tasks;

namespace FactorMaker.Services.ServicesIntefaces
{
    public interface IFactorItemService
    {
        void DeleteById(Guid id);
        Task DeleteByIdAsync(Guid id);
        FactorItem GetById(Guid id);
        Task<FactorItem> GetByIdAsync(Guid id);
        FactorItem Update(Guid id, Guid productId, int quantity, byte offPercent);
        Task<FactorItem> UpdateAsync(Guid id, Guid productId, int quantity, byte offPercent);
    }
}