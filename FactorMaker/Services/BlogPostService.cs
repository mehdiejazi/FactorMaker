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
using ViewModels.BlgoPost;

namespace FactorMaker.Services
{
    public class BlogPostService : BaseServiceWithDatabase, IBlogPostService
    {
        public BlogPostService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<Result> DeleteByIdAsync(Guid id)
        {
            try
            {
                Result result = new Result();
                result.IsSuccessful = true;


                var blogPost = UnitOfWork.BlogPostRepository.GetById(id);
                if (blogPost == null)
                {
                    result.AddErrorMessage(typeof(Category) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                await UnitOfWork.BlogPostRepository.DeleteAsync(blogPost);
                await UnitOfWork.SaveAsync();

                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<BlogPostViewModel>> InsertAsync(BlogPostViewModel viewModel)
        {
            try
            {
                var result = new Result<BlogPostViewModel>();

                var post = viewModel.Adapt<BlogPost>();

                await UnitOfWork.BlogPostRepository.InsertAsync(post);
                await UnitOfWork.SaveAsync();

                result.Data = post.Adapt<BlogPostViewModel>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<BlogPostViewModel>> UpdateAsync(BlogPostViewModel viewModel)
        {
            try
            {
                var result = new Result<BlogPostViewModel>();
                result.IsSuccessful = true;

                BlogPost blogPost = UnitOfWork.BlogPostRepository.GetById(viewModel.Id);
                if (blogPost == null)
                {
                    result.AddErrorMessage(typeof(BlogPost) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                blogPost = viewModel.Adapt<BlogPost>();

                blogPost.UpdateDateTime = DateTime.Now;

                await UnitOfWork.BlogPostRepository.UpdateAsync(blogPost);
                await UnitOfWork.SaveAsync();

                result.Data = blogPost.Adapt<BlogPostViewModel>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<ICollection<BlogPostViewModel>>> GetAllAsync()
        {
            try
            {
                var result = new Result<ICollection<BlogPostViewModel>>();

                var list = await UnitOfWork.BlogPostRepository.GetAllAsync();

                result.Data = list.Adapt<ICollection<BlogPostViewModel>>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<BlogPostViewModel>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = new Result<BlogPostViewModel>();
                result.IsSuccessful = true;

                var blogPost = await UnitOfWork.BlogPostRepository.GetByIdAsync(id);
                if (blogPost == null)
                {
                    result.AddErrorMessage(typeof(BlogPost) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                }

                if (result.IsSuccessful == false) return result;

                result.Data = blogPost.Adapt<BlogPostViewModel>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<ICollection<BlogPostViewModel>>> GetByOwnerIdAsync(Guid ownerId)
        {
            try
            {
                var result = new Result<ICollection<BlogPostViewModel>>();

                var list = await UnitOfWork.BlogPostRepository.GetByOwnerIdAsync(ownerId);

                result.Data = list.Adapt<ICollection<BlogPostViewModel>>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<ICollection<BlogPostViewModel>>> GetByOwnerNotPublishedAsync(Guid ownerId)
        {
            try
            {
                var result = new Result<ICollection<BlogPostViewModel>>();

                var list = await UnitOfWork.BlogPostRepository.GetByOwnerNotPublishedAsync(ownerId);

                result.Data = list.Adapt<ICollection<BlogPostViewModel>>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<ICollection<BlogPostViewModel>>> GetByOwnerPublishedAsync(Guid ownerId)
        {
            try
            {
                var result = new Result<ICollection<BlogPostViewModel>>();

                var list = await UnitOfWork.BlogPostRepository.GetByOwnerPublishedAsync(ownerId);

                result.Data = list.Adapt<ICollection<BlogPostViewModel>>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<ICollection<BlogPostViewModel>>> GetByOwnerPublishedHotAsync(Guid ownerId)
        {
            try
            {
                var result = new Result<ICollection<BlogPostViewModel>>();

                var list = await UnitOfWork.BlogPostRepository.GetByOwnerPublishedHotAsync(ownerId);

                result.Data = list.Adapt<ICollection<BlogPostViewModel>>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<ICollection<BlogPostViewModel>>> GetByPostCategoryIdAsync(Guid postCategoryId)
        {
            try
            {
                var result = new Result<ICollection<BlogPostViewModel>>();

                PostCategory postCategory = await UnitOfWork.PostCategoryRepository.GetByIdAsync(postCategoryId);

                if (postCategory == null)
                {
                    result.AddErrorMessage(typeof(PostCategory) + " " + ErrorMessages.NotFound);
                    result.IsSuccessful = false;
                    return result;
                }

                var list = await UnitOfWork.BlogPostRepository.GetByPostCategoryAsync(postCategory);

                result.Data = list.Adapt<ICollection<BlogPostViewModel>>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<ICollection<BlogPostViewModel>>> GetPublishedAsync()
        {
            try
            {
                var result = new Result<ICollection<BlogPostViewModel>>();

                var list = await UnitOfWork.BlogPostRepository.GetPublishedAsync();

                result.Data = list.Adapt<ICollection<BlogPostViewModel>>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<ICollection<BlogPostViewModel>>> GetPublishedHotAsync()
        {
            try
            {
                var result = new Result<ICollection<BlogPostViewModel>>();

                var list = await UnitOfWork.BlogPostRepository.GetPublishedHotAsync();

                result.Data = list.Adapt<ICollection<BlogPostViewModel>>();
                result.IsSuccessful = true;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Result<ICollection<BlogPostViewModel>>> GetsNotPublishedAsync()
        {
            try
            {
                var result = new Result<ICollection<BlogPostViewModel>>();

                var list = await UnitOfWork.BlogPostRepository.GetsNotPublishedAsync();

                result.Data = list.Adapt<ICollection<BlogPostViewModel>>();
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
