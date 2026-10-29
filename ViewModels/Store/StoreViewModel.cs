using System;
using System.Collections.Generic;
using ViewModels.Base;
using ViewModels.ImageAsset;

namespace ViewModels.Store
{
    public class StoreViewModel : ViewModelBase
    {
        public string StoreEnglishName { get; set; }
        public string Name { get; set; }
        public Guid OwnerId { get; set; }
        public string Url { get; set; }
        public Guid? LogoId { get; set; }
        public ImageAssetViewModel Logo { get; set; }
        public Guid? CoverImageId { get; set; }
        public ImageAssetViewModel CoverImage { get; set; }
        public string Tagline { get; set; }
        public string Description { get; set; }
        public string ThemePalette { get; set; } = "forest";
        public bool IsPublic { get; set; } = true;
        public decimal TaxPercent { get; set; } = 9;
        public ICollection<StoreSocialLinkViewModel> SocialLinks { get; set; }
    }
}
