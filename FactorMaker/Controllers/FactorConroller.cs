using Common;
using FactorMaker.Infrastructure.Attributes;
using FactorMaker.Services.ServicesIntefaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels;

namespace FactorMaker.Controllers
{
    [Authorize]

    public class FactorConroller : BaseApiController
    {
        private FactorConroller() : base()
        {

        }
        public FactorConroller(IFactorService factorService)
        {
            FactorService = factorService;
        }
        private IFactorService FactorService { get; }


        [HttpGet("GetAll")]
        public Result<ICollection<Factor>> GetAll()
        {
            var result = new Result<ICollection<Factor>>();
            result.Data = FactorService.GetAll();
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("GetAllAsync")]
        public async Task<Result<ICollection<Factor>>> GetAllAsync()
        {
            var result = new Result<ICollection<Factor>>();
            result.Data = await FactorService.GetAllAsync();
            result.IsSuccessful = true;

            return result;
        }


        [HttpGet("DeleteById")]
        public Result DeleteById(Guid id)
        {
            FactorService.DeleteById(id);

            var result = new Result();
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("DeleteByIdAsync")]
        public async Task<Result> DeleteByIdAsync(Guid id)
        {
            await FactorService.DeleteByIdAsync(id);

            var result = new Result();
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("GetById")]
        public Result<Factor> GetById(Guid id)
        {
            var result = new Result<Factor>();
            result.Data = FactorService.GetById(id);
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("GetByIdAsync")]
        public async Task<Result<Factor>> GetByIdAsync(Guid id)
        {
            var result = new Result<Factor>();
            result.Data = await FactorService.GetByIdAsync(id);
            result.IsSuccessful = true;

            return result;
        }

        [HttpPost("Insert")]
        public Result<Factor> Insert(FactorViewModel factorViewModel)
        {
            var result = new Result<Factor>();
            result.Data = FactorService.Insert(factorViewModel.OwnerId, factorViewModel.CreatorId);
            result.IsSuccessful = true;

            return result;
        }

        [HttpPost("InsertAsync")]
        public async Task<Result<Factor>> InsertAsync(FactorViewModel factorViewModel)
        {
            var result = new Result<Factor>();
            result.Data = await FactorService.InsertAsync(factorViewModel.OwnerId, factorViewModel.CreatorId);
            result.IsSuccessful = true;

            return result;
        }

        [HttpPost("Update")]
        public Result<Factor> Update(FactorViewModel factorViewModel)
        {
            var result = new Result<Factor>();
            result.Data = FactorService.Update(factorViewModel.Id, factorViewModel.OwnerId);
            result.IsSuccessful = true;

            return result;
        }

        [HttpPost("UpdateAsync")]
        public async Task<Result<Factor>> UpdateAsync(FactorViewModel factorViewModel)
        {
            var result = new Result<Factor>();
            result.Data = await FactorService.UpdateAsync(factorViewModel.Id, factorViewModel.OwnerId);
            result.IsSuccessful = true;

            return result;
        }

        [HttpPost("InsertFactorItem")]
        public Result<FactorItem> InsertFactorItem(FactorItemViewModel factorItemViewModel)
        {
            var result = new Result<FactorItem>();
            result.Data = FactorService.InsertFactorItem(
                factorItemViewModel.Id, factorItemViewModel.productId,
                factorItemViewModel.quantity, factorItemViewModel.offpercent);
            result.IsSuccessful = true;

            return result;
        }

        [HttpPost("InsertFactorItemAsync")]
        public async Task<Result<FactorItem>> InsertFactorItemAsync(FactorItemViewModel factorItemViewModel)
        {
            var result = new Result<FactorItem>();
            result.Data = await FactorService.InsertFactorItemAsync(
                factorItemViewModel.Id, factorItemViewModel.productId,
                factorItemViewModel.quantity, factorItemViewModel.offpercent);
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("CalculateFactorById")]
        public Result<TotalFactorViewModel> CalculateFactorById(Guid id)
        {
            var result = new Result<TotalFactorViewModel>();
            result.Data = FactorService.CalculateFactorById(id);
            result.IsSuccessful = true;

            return result;
        }

        [HttpGet("CalculateFactorByIdAsync")]
        public async Task<Result<TotalFactorViewModel>> CalculateFactorByIdAsync(Guid id)
        {
            var result = new Result<TotalFactorViewModel>();
            result.Data = await FactorService.CalculateFactorByIdAsync(id);
            result.IsSuccessful = true;

            return result;
        }
    }
}
