using Models.Base;
using System;
using System.Collections.Generic;

namespace Models
{
    public class Store : EntityBase
    {
        public string StoreEnglishName { get; set; }
        public string Name { get; set; }
        public User Owner { get; set; }
        public Guid OwnerId { get; set; }
        public string Url { get; set; }
        public ImageAsset Logo { get; set; }
        public Guid? LogoId { get; set; }
        public ImageAsset CoverImage { get; set; }
        public Guid? CoverImageId { get; set; }
        public string Tagline { get; set; }
        public string Description { get; set; }
        public string ThemePalette { get; set; } = "forest";
        public bool IsPublic { get; set; } = true;
        public virtual ICollection<Factor> Factors { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<StoreSocialLink> SocialLinks { get; set; }

    }
}
