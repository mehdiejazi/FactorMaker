using System;
using ViewModels.Base;

namespace ViewModels.FactorItem
{
    public class FactorItemViewModel : ViewModelBase
    {
        public Guid factorId { get; set; }
        public Guid productId { get; set; }
        public int quantity { get; set; }
        public byte offpercent { get; set; }
    }
}
