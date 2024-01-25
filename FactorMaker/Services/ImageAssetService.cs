using Common;
using Data;
using FactorMaker.Services.Base;
using FactorMaker.Services.ServiceIntefaces;
using Mapster;
using Microsoft.AspNetCore.Http;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ViewModels.ImageAsset;
using ViewModels.User;

namespace FactorMaker.Services
{
    public class ImageAssetService : BaseServiceWithDatabase, IImageAssetService
    {
        private readonly string _uploadImageDir = "uploads/images";

        public ImageAssetService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public async Task<Result<ICollection<ImageAssetViewModel>>> GetDeletedByUserIdAsync(Guid userId)
        {
            try
            {
                var result = new Result<ICollection<ImageAssetViewModel>>();
                result.IsSuccessful = true;

                User user = await UnitOfWork.UserRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    result.AddErrorMessage(nameof(user) + Resources.ErrorMessages.NotFound);
                    result.IsSuccessful = false;

                    return result;
                }

                var list = (List<ImageAsset>)await UnitOfWork.ImageAssetRepository.GetDeletedByUserAsync(user);

                var listViewModel = new List<ImageAssetViewModel>();

                foreach (var item in list)
                {
                    ImageAssetViewModel viewModel = new ImageAssetViewModel()
                    {
                        FileName = item.FileName,
                        Id = item.Id,
                        InsertDateTime = item.InsertDateTime,
                        IsDeleted = item.IsDeleted,
                        OwnerUser = item.Owner.Adapt<UserViewModel>(),
                        Url = item.Url
                    };

                    listViewModel.Add(viewModel);
                }

                result.Data = listViewModel;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Result<ICollection<ImageAssetViewModel>>> GetNotDeletedByUserIdAsync(Guid userId)
        {
            try
            {
                var result = new Result<ICollection<ImageAssetViewModel>>();
                result.IsSuccessful = true;

                User user = await UnitOfWork.UserRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    result.AddErrorMessage(nameof(user) + Resources.ErrorMessages.NotFound);
                    result.IsSuccessful = false;

                    return result;
                }

                var list = (List<ImageAsset>)await UnitOfWork.ImageAssetRepository.GetNotDeletedByUserAsync(user);

                var listViewModel = new List<ImageAssetViewModel>();

                foreach (var item in list)
                {
                    ImageAssetViewModel viewModel = new ImageAssetViewModel()
                    {
                        FileName = item.FileName,
                        Id = item.Id,
                        InsertDateTime = item.InsertDateTime,
                        IsDeleted = item.IsDeleted,
                        OwnerUser = item.Owner.Adapt<UserViewModel>(),
                        Url = item.Url
                    };

                    listViewModel.Add(viewModel);
                }

                result.Data = listViewModel;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Result<ICollection<ImageAssetViewModel>>> GetByUserIdAsync(Guid userId)
        {
            try
            {
                var result = new Result<ICollection<ImageAssetViewModel>>();
                result.IsSuccessful = true;

                User user = await UnitOfWork.UserRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    result.AddErrorMessage(nameof(user) + Resources.ErrorMessages.NotFound);
                    result.IsSuccessful = false;

                    return result;
                }


                var list = (List<ImageAsset>)await UnitOfWork.ImageAssetRepository.GetByUserAsync(user);

                var listViewModel = new List<ImageAssetViewModel>();

                foreach (var item in list)
                {
                    ImageAssetViewModel viewModel = new ImageAssetViewModel()
                    {
                        FileName = item.FileName,
                        Id = item.Id,
                        InsertDateTime = item.InsertDateTime,
                        IsDeleted = item.IsDeleted,
                        OwnerUser = item.Owner.Adapt<UserViewModel>(),
                        Url = item.Url
                    };

                    listViewModel.Add(viewModel);
                }

                result.Data = listViewModel;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Result<ImageAssetViewModel>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = new Result<ImageAssetViewModel>();

                var entity = await UnitOfWork.ImageAssetRepository.GetByIdAsync(id);

                if (entity == null)
                {
                    result.AddErrorMessage(typeof(ImageAsset) + Resources.ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }
                else
                {
                    result.Data = entity.Adapt<ImageAssetViewModel>();
                    result.IsSuccessful = true;
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Result<ImageAssetViewModel>> InsertAsync(IFormFile imageDataFile, string fileName, Guid userId)
        {
            try
            {
                Result<ImageAssetViewModel> result = new Result<ImageAssetViewModel>();
                result.IsSuccessful = true;

                User user = await UnitOfWork.GetRepository<User>().GetByIdAsync(userId);

                if (user == null)
                {
                    result.AddErrorMessage(typeof(User) + Resources.ErrorMessages.NotFound);
                    result.IsSuccessful = false;

                    return result;
                }

                string fileNamFullPath = _uploadImageDir + "/" + fileName;

                if (File.Exists(fileNamFullPath))
                {
                    Random rnd = new Random();

                    string flnm = Path.GetFileNameWithoutExtension(_uploadImageDir + "/" + fileName);
                    string ext = Path.GetExtension(_uploadImageDir + "/" + fileName);

                    var rndnum = rnd.Next(1, 999999).ToString("000000");

                    fileNamFullPath = _uploadImageDir + "/" +
                        flnm + "_" + rndnum + ext;

                    fileName = flnm + "_" + rndnum + ext;
                }

                var parsedContentDisposition = ContentDispositionHeaderValue.Parse(imageDataFile.ContentDisposition);

                using (var stream = File.OpenWrite(fileNamFullPath))
                {
                    await imageDataFile.CopyToAsync(stream);
                }


                ImageAsset image = new ImageAsset();
                image.FileName = fileName;
                image.Owner = user;
                image.Url = "/" + _uploadImageDir + "/" + fileName;

                await UnitOfWork.ImageAssetRepository.InsertAsync(image);
                await UnitOfWork.SaveAsync();

                result.Data = image.Adapt<ImageAssetViewModel>();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Result> DeleteByIdUserIdAsync(Guid id, Guid userId)
        {
            try
            {
                Result result = new Result();
                result.IsSuccessful = true;

                var image = await UnitOfWork.ImageAssetRepository.GetByIdAsync(id);

                if (image == null)
                {
                    result.AddErrorMessage(typeof(ImageAsset) + " " + Resources.ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }
                if (image.Owner.Id != userId)
                {
                    result.AddErrorMessage(Resources.ErrorMessages.UnauthorizedAccess);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                image.IsDeleted = true;

                await UnitOfWork.ImageAssetRepository.UpdateAsync(image);
                await UnitOfWork.SaveAsync();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
