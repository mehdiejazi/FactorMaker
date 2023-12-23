using System;
using System.Collections.Generic;
using ViewModels.Base;
using ViewModels.Customer;
using ViewModels.FactorItem;
using ViewModels.User;

namespace ViewModels.Factor
{
    public class FactorViewModel : ViewModelBase
    {
        public CustomerViewModel Owner { get; set; }
        public Guid OwnerId { get; set; }
        public UserViewModel Creator { get; set; }
        public Guid CreatorId { get; set; }
        public string Description { get; set; }
        public virtual ICollection<FactorItemViewModel> FactorItems { get; set; }
    }
}
