using Data.Repositories;
using Data.Repositories.RepositoryInterfaces;

namespace Data
{
    public interface IUnitOfWork : Base.IUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; }
        IFactorItemRepository FactorItemRepository { get; }
        IFactorRepository FactorRepository { get; }
        IProductRepository ProductRepository { get; }
        IUserRepository UserRepository { get; }
    }
}
