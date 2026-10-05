using System;
using ViewModels.Base;

namespace ViewModels.Store
{
    public class StoreSocialLinkViewModel : ViewModelBase
    {
        public Guid StoreId { get; set; }
        public string Platform { get; set; }
        public string Url { get; set; }
        public string Label { get; set; }
        public bool IsPrimary { get; set; }
    }
}
