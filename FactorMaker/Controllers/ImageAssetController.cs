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
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            var result = await ImageAssetService.DeleteByIdUserIdAsync(id, User.Id);
            return Result(result);
        }

        [HttpGet("GetAsync")]
        public async Task<IActionResult> GetAsync()
        {
            var result = await ImageAssetService.GetByUserIdAsync(User.Id);
            return Result(result);
        }

        [HttpGet("GetDeletedAsync")]
        public async Task<IActionResult> GetDeletedAsync()
        {
            var result = await ImageAssetService.GetDeletedByUserIdAsync(User.Id);
            return Result(result);
        }

        [HttpGet("GetNotDeletedAsync")]
        public async Task<IActionResult> GetNotDeletedAsync()
        {
            var result = await ImageAssetService.GetNotDeletedByUserIdAsync(User.Id);
            return Result(result);
        }

        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await ImageAssetService.GetByIdAsync(id);
            return Result(result);
        }

        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync(IFormFile imageDataFile,string fileName)
        {
            var result = await ImageAssetService.InsertAsync(imageDataFile, fileName, User.Id);
            return Result(result);
        }
        
    }
}

