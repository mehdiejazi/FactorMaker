using System;
using ViewModels.Base;
using ViewModels.Category;
using ViewModels.Store;
using ViewModels.ImageAsset;

namespace ViewModels.Product
{
    public class ProductViewModel : ViewModelBase
    {
       public StoreViewModel Store { get; set; }
        public Guid StoreId { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
        public Guid? ImageId { get; set; }
        public ImageAssetViewModel Image { get; set; }
        public string ShortDescription { get; set; }
        public bool ShowOnLanding { get; set; }
        public int SortOrder { get; set; }
        public CategoryViewModel Category { get; set; }
        public Guid CategoryId { get; set; }
    }
}
