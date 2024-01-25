using Common;
using Data;
using FactorMaker.Services.Base;
using FactorMaker.Services.ServicesIntefaces;
using Mapster;
using Models;
using Resources;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.PostCategory;

namespace FactorMaker.Services
{
    public class PostCategoryService : BaseServiceWithDatabase, IPostCategoryService
    {
        public PostCategoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<Result<PostCategoryViewModel>> InsertAsync(PostCategoryViewModel viewModel)
        {
            try
            {
                var result = new Result<PostCategoryViewModel>();

                var cat = viewModel.Adapt<PostCategory>();

                await UnitOfWork.PostCategoryRepository.InsertAsync(cat);
                await UnitOfWork.SaveAsync();

                result.Data = cat.Adapt<PostCategoryViewModel>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<PostCategoryViewModel>> UpdateAsync(PostCategoryViewModel viewModel)
        {
            try
            {
                var result = new Result<PostCategoryViewModel>();
                result.IsSuccessful = true;

                PostCategory postCategory = await UnitOfWork.PostCategoryRepository.GetByIdAsync(viewModel.Id);
                if (postCategory == null)
                {
                    result.AddErrorMessage(typeof(PostCategory) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                postCategory = viewModel.Adapt<PostCategory>();

                postCategory.UpdateDateTime = DateTime.Now;

                await UnitOfWork.PostCategoryRepository.UpdateAsync(postCategory);
                await UnitOfWork.SaveAsync();

                result.Data = postCategory.Adapt<PostCategoryViewModel>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result> DeleteByIdAsync(Guid id)
        {
            try
            {
                Result result = new Result();
                result.IsSuccessful = true;


                var postCat = UnitOfWork.PostCategoryRepository.GetById(id);
                if (postCat == null)
                {
                    result.AddErrorMessage(typeof(Category) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                await UnitOfWork.PostCategoryRepository.DeleteAsync(postCat);
                await UnitOfWork.SaveAsync();

                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<PostCategoryViewModel>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = new Result<PostCategoryViewModel>();
                result.IsSuccessful = true;

                var postCategory = await UnitOfWork.PostCategoryRepository.GetByIdAsync(id);
                if (postCategory == null)
                {
                    result.AddErrorMessage(typeof(PostCategory) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                result.Data = postCategory.Adapt<PostCategoryViewModel>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<ICollection<PostCategoryViewModel>>> GetAllAsync()
        {
            try
            {
                var result = new Result<ICollection<PostCategoryViewModel>>();

                var list = await UnitOfWork.BlogPostRepository.GetAllAsync();

                result.Data = list.Adapt<ICollection<PostCategoryViewModel>>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<ICollection<PostCategoryViewModel>>> GetByOwnerIdAsync(Guid ownerId)
        {
            try
            {
                var result = new Result<ICollection<PostCategoryViewModel>>();

                var postCategories = await UnitOfWork.PostCategoryRepository.GetByOwnerIdAsync(ownerId);

                result.Data = postCategories.Adapt<ICollection<PostCategoryViewModel>>();
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

