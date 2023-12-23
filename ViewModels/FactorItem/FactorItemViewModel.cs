using System;
using ViewModels.Base;
using ViewModels.Factor;
using ViewModels.Product;

namespace ViewModels.FactorItem
{
    public class FactorItemViewModel : ViewModelBase
    {
        public FactorViewModel Factor { get; set; }
        public Guid factorId { get; set; }
        public ProductViewModel Product { get; set; }
        public Guid productId { get; set; }
        public int quantity { get; set; }
        public byte offpercent { get; set; }
    }
}
