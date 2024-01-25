using Models;
using System;
using ViewModels.Base;
using ViewModels.Category;
using ViewModels.User;

namespace ViewModels.Product
{
    public class ProductViewModel : ViewModelBase
    {
        public UserViewModel Owner { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
        public CategoryViewModel Category { get; set; }
        public Guid CategoryId { get; set; }
    }
}
