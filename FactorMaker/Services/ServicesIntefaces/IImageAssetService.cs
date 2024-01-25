using Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.ImageAsset;

namespace FactorMaker.Services.ServiceIntefaces
{
    public interface IImageAssetService
    {
        Task<Result> DeleteByIdUserIdAsync(Guid id, Guid userId);
        Task<Result<ICollection<ImageAssetViewModel>>> GetByUserIdAsync(Guid userId);
        Task<Result<ICollection<ImageAssetViewModel>>> GetDeletedByUserIdAsync(Guid userId);
        Task<Result<ICollection<ImageAssetViewModel>>> GetNotDeletedByUserIdAsync(Guid userId);
        Task<Result<ImageAssetViewModel>> GetByIdAsync(Guid id);
        Task<Result<ImageAssetViewModel>> InsertAsync(IFormFile imageDataFile, string fileName, Guid userId);

    }
}