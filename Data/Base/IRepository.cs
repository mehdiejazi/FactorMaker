using System.Threading.Tasks;

namespace Data.Base
{
    public interface IRepository<T> where T : Models.Base.EntityBase
    {
        void Insert(T entity);

        Task InsertAsync(T entity);

        void Update(T entity);

        Task UpdateAsync(T entity);

        void Delete(T entity);

        Task DeleteAsync(T entity);

        T GetById(System.Guid id);

        Task<T> GetByIdAsync(System.Guid id);

        bool DeleteById(System.Guid id);

        Task<bool> DeleteByIdAsync(System.Guid id);

        System.Collections.Generic.ICollection<T> GetAll();

        Task<System.Collections.Generic.ICollection<T>> GetAllAsync();
    }
}
