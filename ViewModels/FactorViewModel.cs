using System;
using ViewModels.Base;

namespace ViewModels
{
    public class FactorViewModel:ViewModelBase
    {
        public Guid OwnerId { get; set; }
        public Guid CreatorId { get; set; }
    }
}
