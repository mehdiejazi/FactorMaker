﻿using ViewModels.Base;
using ViewModels.User;

namespace ViewModels.Customer
{
    public class CustomerViewModel : PersonViewModelBase
    {
        public UserViewModel Owner { get; set; }
    }
}
