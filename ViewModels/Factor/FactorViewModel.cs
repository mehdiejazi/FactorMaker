using System;
using ViewModels.Base;

namespace ViewModels.Factor
{
    public class FactorViewModel : ViewModelBase
    {
        public Guid OwnerId { get; set; }
        public Guid CreatorId { get; set; }
    }
}
