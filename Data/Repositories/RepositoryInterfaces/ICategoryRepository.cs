using Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Data.DataTransferObjects.Category;

namespace Data.Repositories.RepositoryInterfaces
{
    public interface ICategoryRepository : Data.Base.IRepository<Category>
    {
        Task<ICollection<Category>> GetByStoreIdAsync(Guid ownerId);
        Task<ICollection<CategorySaleTotalPriceDto>> GetSaleTotalByPriceAsync(DateTime dtFrom, DateTime dtTo, Guid StoreId);
        Task<ICollection<CategorySaleTotalQuantityDto>> GetSaleTotalByQuantityAsync(DateTime dtFrom, DateTime dtTo, Guid StoreId);


        //* Pie Chart: Category Sold Count By Store.
        //* Pie Chart: Category Sold Sum of price By Store
    }
}
