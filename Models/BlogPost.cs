using Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class BlogPost : EntityBase
    {
        public BlogPost() : base()
        {

        }

        public virtual ICollection<PostCategory> PostCategories { get; set; }
        public virtual User Owner { get; set; }
        public Guid OwnerId { get; set; }
        public int VisitCount { get; set; }
        public bool IsHot { get; set; }
        public bool IsPublished { get; set; }
        public bool IsStarRatingEnabled { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string ExtendedBody { get; set; }
        public DateTime PublishDateTime { get; set; }
        public virtual ImageAsset CoverImage { get; set; }
        public Guid? CoverImageId { get; set; }
        public virtual ICollection<ImageAsset> Images { get; set; }
        public double StarRate { get; set; }
        //public virtual ICollection<StarRating> StarRatings { get; set; }

        //public virtual ICollection<BlogPost> RelatedBlogPosts { get; set; }
    }
}