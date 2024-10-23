using System;
using ViewModels.Base;
using ViewModels.Category;
using ViewModels.Store;

namespace ViewModels.Product
{
    public class ProductViewModel : ViewModelBase
    {
       public StoreViewModel Store { get; set; }
        public Guid StoreId { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
        public CategoryViewModel Category { get; set; }
        public Guid CategoryId { get; set; }
    }
}
