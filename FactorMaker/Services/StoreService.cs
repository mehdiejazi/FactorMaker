using Common;
using Data;
using FactorMaker.Services.Base;
using FactorMaker.Services.ServiceIntefaces;
using Mapster;
using Models;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
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
                result.Data = list.Adapt<ICollection<StoreViewModel>>();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Result<ICollection<StoreViewModel>>> GetActiveAsync()
        {
            try
            {
                var result = new Result<ICollection<StoreViewModel>>();
                result.IsSuccessful = true;

                var list = await UnitOfWork.StoreRepository.GetActiveAsync();
                result.Data = list.Adapt<ICollection<StoreViewModel>>();
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
                if (store == null || !store.IsPublic)
                {
                    result.AddErrorMessage(typeof(Store) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                result.Data = store.Adapt<StoreViewModel>();
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
                store.Owner = null;
                store.Logo = null;
                store.CoverImage = null;
                store.Factors = null;
                store.Products = null;
                store.Categories = null;
                store.LogoId = viewModel.LogoId ?? viewModel.Logo?.Id;
                store.CoverImageId = viewModel.CoverImageId ?? viewModel.CoverImage?.Id;
                store.ThemePalette = NormalizePalette(viewModel.ThemePalette);
                store.IsPublic = viewModel.IsPublic;
                store.TaxPercent = NormalizeTaxPercent(viewModel.TaxPercent);
                store.SocialLinks = new List<StoreSocialLink>();

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
                foreach (var socialLink in BuildSocialLinks(viewModel.SocialLinks, store.Id))
                {
                    store.SocialLinks.Add(socialLink);
                }
                store.Url = store.SocialLinks.FirstOrDefault(x => x.IsPrimary)?.Url ?? viewModel.Url;
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
                if (engStore != null && engStore.Id.Equals(viewModel.Id) == false)
                {
                    result.IsSuccessful = false;
                    result.AddErrorMessage(nameof(viewModel.StoreEnglishName) + " " + ErrorMessages.AlreadyExists);
                    return result;
                }

                if (result.IsSuccessful == false) return result;

                store.IsDeleted = viewModel.IsDeleted;
                store.LogoId = viewModel.LogoId;
                store.CoverImageId = viewModel.CoverImageId;
                store.Name = viewModel.Name;
                store.OwnerId = viewModel.OwnerId;
                store.Url = viewModel.Url;
                store.StoreEnglishName = viewModel.StoreEnglishName;
                store.Description = viewModel.Description;
                store.Tagline = viewModel.Tagline;
                store.ThemePalette = NormalizePalette(viewModel.ThemePalette);
                store.IsPublic = viewModel.IsPublic;
                store.TaxPercent = NormalizeTaxPercent(viewModel.TaxPercent);

                if (store.SocialLinks == null)
                {
                    store.SocialLinks = new List<StoreSocialLink>();
                }

                store.SocialLinks.Clear();
                foreach (var socialLink in BuildSocialLinks(viewModel.SocialLinks, store.Id))
                {
                    store.SocialLinks.Add(socialLink);
                }
                store.Url = store.SocialLinks.FirstOrDefault(x => x.IsPrimary)?.Url ?? viewModel.Url;

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

        private static ICollection<StoreSocialLink> BuildSocialLinks(
            ICollection<StoreSocialLinkViewModel> links,
            Guid storeId)
        {
            var normalizedLinks = (links ?? new List<StoreSocialLinkViewModel>())
                .Where(x => !string.IsNullOrWhiteSpace(x.Url))
                .Select(x => new StoreSocialLink
                {
                    StoreId = storeId,
                    Platform = (x.Platform ?? string.Empty).Trim(),
                    Url = x.Url.Trim(),
                    Label = (x.Label ?? string.Empty).Trim(),
                    IsPrimary = x.IsPrimary
                })
                .ToList();

            if (normalizedLinks.Count > 0 && normalizedLinks.All(x => !x.IsPrimary))
            {
                normalizedLinks[0].IsPrimary = true;
            }

            if (normalizedLinks.Count(x => x.IsPrimary) > 1)
            {
                var primary = normalizedLinks.First(x => x.IsPrimary);
                foreach (var link in normalizedLinks.Where(x => x != primary))
                {
                    link.IsPrimary = false;
                }
            }

            return normalizedLinks;
        }

        private static decimal NormalizeTaxPercent(decimal taxPercent)
        {
            if (taxPercent < 0) return 0;
            if (taxPercent > 100) return 100;
            return decimal.Round(taxPercent, 2);
        }

        private static string NormalizePalette(string palette)
        {
            var supported = new[] { "forest", "ocean", "burgundy", "indigo", "graphite" };
            return supported.Contains(palette) ? palette : "forest";
        }
    }
}
