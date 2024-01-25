using ViewModels.Base;
using ViewModels.User;

namespace ViewModels.ImageAsset
{
    public class ImageAssetViewModel : ViewModelBase
    {
        public string FileName { get; set; }
        public string Url { get; set; }
        public UserViewModel OwnerUser { get; set; }
    }
}