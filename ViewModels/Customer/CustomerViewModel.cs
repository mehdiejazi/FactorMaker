using System;
using ViewModels.Base;
using ViewModels.Store;
using ViewModels.User;

namespace ViewModels.Customer
{
    public class CustomerViewModel : PersonViewModelBase
    {
        public StoreViewModel Store { get; set; }
        public Guid StoreId { get; set; }
    }
}
