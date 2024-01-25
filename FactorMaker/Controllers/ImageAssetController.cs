using Common;
using FactorMaker.Services.ServiceIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.ImageAsset;

namespace FactorMaker.Controllers
{
    public class ImageAssetController : BaseApiControllerWithUser
    {
        private ImageAssetController() : base()
        {

        }
        public ImageAssetController(IImageAssetService imageAssetService)
        {
            ImageAssetService = imageAssetService;
        }
        private IImageAssetService ImageAssetService { get; set; }

        [HttpGet("DeleteByIdAsync")]
        public Task<Result> DeleteByIdAsync(Guid id)
        {
            var result = ImageAssetService.DeleteByIdUserIdAsync(id, User.Id);
            return result;
        }

        [HttpGet("GetAsync")]
        public Task<Result<ICollection<ImageAssetViewModel>>> GetAsync()
        {
            var result = ImageAssetService.GetByUserIdAsync(User.Id);
            return result;
        }

        [HttpGet("GetDeletedAsync")]
        public Task<Result<ICollection<ImageAssetViewModel>>> GetDeletedAsync()
        {
            var result = ImageAssetService.GetDeletedByUserIdAsync(User.Id);
            return result;
        }

        [HttpGet("GetNotDeletedAsync")]
        public Task<Result<ICollection<ImageAssetViewModel>>> GetNotDeletedAsync()
        {
            var result = ImageAssetService.GetNotDeletedByUserIdAsync(User.Id);
            return result;
        }

        [HttpGet("GetByIdAsync")]
        public Task<Result<ImageAssetViewModel>> GetByIdAsync(Guid id)
        {
            var result = ImageAssetService.GetByIdAsync(id);
            return result;
        }

        [HttpPost("InsertAsync")]
        public Task<Result<ImageAssetViewModel>> InsertAsync(IFormFile imageDataFile,string fileName)
        {
            var result = ImageAssetService.InsertAsync(imageDataFile, fileName, User.Id);
            return result;
        }
        
    }
}

