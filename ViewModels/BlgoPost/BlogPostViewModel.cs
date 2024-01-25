using Models.Base;
using System;
using System.Collections.Generic;
using ViewModels.Base;
using ViewModels.ImageAsset;
using ViewModels.PostCategory;
using ViewModels.User;

namespace ViewModels.BlgoPost
{
    public class BlogPostViewModel : ViewModelBase
    {
        public BlogPostViewModel() : base()
        {

        }

        public virtual ICollection<PostCategoryViewModel> PostCategories { get; set; }
        public virtual UserViewModel Owner { get; set; }
        public Guid OwnerId { get; set; }
        public int VisitCount { get; set; }
        public bool IsHot { get; set; }
        public bool IsPublished { get; set; }
        public bool IsStarRatingEnabled { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string ExtendedBody { get; set; }
        public DateTime PublishDateTime { get; set; }
        public virtual ImageAssetViewModel CoverImage { get; set; }
        public virtual ICollection<ImageAssetViewModel> Images { get; set; }
        public double StarRate { get; set; }
        //public virtual ICollection<StarRating> StarRatings { get; set; }

        //public virtual ICollection<BlogPost> RelatedBlogPosts { get; set; }
    }
}