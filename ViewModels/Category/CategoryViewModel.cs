using System;
using ViewModels.Base;
using ViewModels.Store;

namespace ViewModels.Category
{
    public class CategoryViewModel : ViewModelBase
    {
        public StoreViewModel Store { get; set; }
        public Guid StoreId { get; set; }
        public string Name { get; set; }
    }
}
