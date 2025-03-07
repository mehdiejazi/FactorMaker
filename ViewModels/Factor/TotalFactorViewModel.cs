﻿using System.Collections.Generic;
using ViewModels.FactorItem;

namespace ViewModels.Factor
{
    public class TotalFactorViewModel : FactorViewModel
    {
        public string OwnerFullName { get; set; }
        public string CreatorFullName { get; set; }
        public ICollection<TotalFactorItemViewModel> FatorItems { get; set; }
    }
}
