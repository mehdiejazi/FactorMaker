using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories.RepositoryInterfaces
{
    public interface IImageAssetRepository : Data.Base.IRepository<ImageAsset>
    {
        new Task<ICollection<ImageAsset>> GetAllAsync();
        new Task<ImageAsset> GetByIdWithRefreshTokenAsync(Guid Id);
        Task<ICollection<ImageAsset>> GetByUserAsync(User user);
        Task<ICollection<ImageAsset>> GetNotDeletedByUserAsync(User user);
        Task<ICollection<ImageAsset>> GetDeletedByUserAsync(User user);

        Task<ICollection<ImageAsset>> GetByRoleAsync(Role role);
        Task<ICollection<ImageAsset>> GetNotDeletedByRoleAsync(Role role);
        Task<ICollection<ImageAsset>> GetDeletedByRoleAsync(Role role);

    }
}
