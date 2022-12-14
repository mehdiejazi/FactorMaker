using System.Collections.Generic;

namespace ViewModels
{
    public class TotalFactorViewModel : FactorViewModel
    {
        public long TotalPrice { get; set; }
        public string OwnerFullName { get; set; }
        public string CreatorFullName { get; set; }
        public ICollection<TotalFactorItemViewModel> FatorItems { get; set; }
    }
}
