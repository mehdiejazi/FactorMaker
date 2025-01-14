﻿using System;
using ViewModels.Base;
using ViewModels.ImageAsset;

namespace ViewModels.Store
{
    public class StoreViewModel : ViewModelBase
    {
        public string StoreEnglishName { get; set; }
        public string Name { get; set; }
        public Guid OwnerId { get; set; }
        public string Url { get; set; }
        public Guid? LogoId { get; set; }
        public ImageAssetViewModel Logo { get; set; }
        public string Description { get; set; }
    }
}
