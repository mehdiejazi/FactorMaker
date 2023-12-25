using Models.Base;
using System;
using ViewModels.Base;
using ViewModels.User;

namespace ViewModels.Store
{
    public class StoreViewModel : ViewModelBase
    {
        public string StoreId { get; set; }
        public string Name { get; set; }
        public UserViewModel Owner { get; set; }
        public Guid OwnerId { get; set; }
        public string Url { get; set; }
        public string LogoUrl { get; set; }
        public string Description { get; set; }
    }
}
