using System;
using System.Collections.Generic;
using ViewModels.Base;
using ViewModels.Customer;
using ViewModels.FactorItem;
using ViewModels.Store;

namespace ViewModels.Factor
{
    public class FactorViewModel : ViewModelBase
    {
        public virtual CustomerViewModel Owner { get; set; }
        public Guid OwnerId { get; set; }
        public virtual StoreViewModel Store { get; set; }
        public Guid StoreId { get; set; }
        public string Description { get; set; }
        public virtual ICollection<FactorItemViewModel> FactorItems { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsClosed { get; set; }
    }
}
