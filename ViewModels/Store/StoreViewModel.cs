using System;
using ViewModels.Base;
using ViewModels.ImageAsset;
using ViewModels.User;

namespace ViewModels.Store
{
    public class StoreViewModel : ViewModelBase
    {
        public string EnglishName { get; set; }
        public string Name { get; set; }
        public UserViewModel Owner { get; set; }
        public Guid OwnerId { get; set; }
        public string Url { get; set; }
        public ImageAssetViewModel Logo { get; set; }
        public Guid LogoId { get; set; }
        public string Description { get; set; }
    }
}
