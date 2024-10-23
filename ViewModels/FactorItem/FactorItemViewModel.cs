using System;
using ViewModels.Base;
using ViewModels.Product;

namespace ViewModels.FactorItem
{
    public class FactorItemViewModel : ViewModelBase
    {
        public string Description { get; set; }
        //public FactorViewModel Factor { get; set; }
        public Guid FactorId { get; set; }
        public ProductViewModel Product { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public int OffPercent { get; set; }
        public decimal Price { get; set; }
    }
}
