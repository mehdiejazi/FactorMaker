using System;

namespace ViewModels.ImageAsset
{
    public class ImageAssetAdminFilterRequestViewModel
    {
        public Guid? OwnerId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
