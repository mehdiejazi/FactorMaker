﻿using Common;
using Data;
using FactorMaker.Services.Base;
using FactorMaker.Services.ServiceIntefaces;
using Mapster;
using Models;
using Resources;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Store;

namespace FactorMaker.Services
{
    public class StoreService : BaseServiceWithDatabase, IStoreService
    {
        public StoreService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<Result> DeleteByIdAsync(Guid id)
        {
            try
            {
                var result = new Result();
                result.IsSuccessful = true;

                var store = await UnitOfWork.StoreRepository.GetByIdAsync(id);
                if (store == null)
                {
                    result.AddErrorMessage(typeof(Store) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                await UnitOfWork.StoreRepository.DeleteAsync(store);
                await UnitOfWork.SaveAsync();

                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<StoreViewModel>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = new Result<StoreViewModel>();

                result.IsSuccessful = true;

                var store = await UnitOfWork.StoreRepository.GetByIdAsync(id);
                if (store == null)
                {
                    result.AddErrorMessage(typeof(Store) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                result.Data = store.Adapt<StoreViewModel>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<ICollection<StoreViewModel>>> GetByOwnerIdAsync(Guid ownerId)
        {
            try
            {
                var result = new Result<ICollection<StoreViewModel>>();

                result.IsSuccessful = true;

                var list = await UnitOfWork.StoreRepository.GetByOwnerIdAsync(ownerId);

                if (result.IsSuccessful == false) return result;

                result.Data = list.Adapt<ICollection<StoreViewModel>>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<StoreViewModel>> GetByStoreEnglishNameAsync(string storeEnglishName)
        {
            try
            {
                var result = new Result<StoreViewModel>();

                result.IsSuccessful = true;

                var store = await UnitOfWork.StoreRepository.GetByStoreEnglishNameAsync(storeEnglishName);
                if (store == null)
                {
                    result.AddErrorMessage(typeof(Store) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                result.Data = store.Adapt<StoreViewModel>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<StoreViewModel>> InsertAsync(StoreViewModel viewModel)
        {
            try
            {
                var result = new Result<StoreViewModel>();

                var store = viewModel.Adapt<Store>();

                var owner = await UnitOfWork.UserRepository.GetByIdAsync(viewModel.OwnerId);
                if (owner == null)
                {
                    result.IsSuccessful = false;
                    result.AddErrorMessage(nameof(viewModel.OwnerId) + " " + ErrorMessages.NotFound);
                    return result;
                }


                if (await UnitOfWork.StoreRepository.IsExistByStoreEnglishNameAsync(viewModel.StoreEnglishName))
                {
                    result.IsSuccessful = false;
                    result.AddErrorMessage(nameof(viewModel.StoreEnglishName) + " " + ErrorMessages.AlreadyExists);
                    return result;
                }

                await UnitOfWork.StoreRepository.InsertAsync(store);
                await UnitOfWork.SaveAsync();

                result.Data = store.Adapt<StoreViewModel>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<StoreViewModel>> UpdateAsync(StoreViewModel viewModel)
        {
            try
            {
                var result = new Result<StoreViewModel>();
                result.IsSuccessful = true;

                var store = await UnitOfWork.StoreRepository.GetByIdAsync(viewModel.Id);
                if (store == null)
                {
                    result.AddErrorMessage(typeof(Store) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                var engStore = await UnitOfWork.StoreRepository.GetByStoreEnglishNameAsync(viewModel.StoreEnglishName);
                if (engStore != null)
                    if (engStore.Id.Equals(viewModel.Id) == false)
                    {
                        result.IsSuccessful = false;
                        result.AddErrorMessage(nameof(viewModel.StoreEnglishName) + " " + ErrorMessages.AlreadyExists);
                        return result;
                    }

                if (result.IsSuccessful == false) return result;

                store.IsDeleted = viewModel.IsDeleted;
                store.LogoId = viewModel.LogoId;
                store.Name = viewModel.Name;
                store.OwnerId = viewModel.OwnerId;
                store.Url = viewModel.Url;
                //store.Logo = await UnitOfWork.ImageAssetRepository.GetByIdAsync();

                await UnitOfWork.StoreRepository.UpdateAsync(store);
                await UnitOfWork.SaveAsync();

                result.Data = store.Adapt<StoreViewModel>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
