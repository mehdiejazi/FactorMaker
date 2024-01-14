using System;
using ViewModels.Base;
using ViewModels.User;

namespace Models
{
    public class CategoryViewModel : ViewModelBase
    {
        public UserViewModel Owner { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
    }
}
