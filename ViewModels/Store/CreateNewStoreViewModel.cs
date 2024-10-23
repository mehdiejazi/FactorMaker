using System;

namespace ViewModels.Store
{
    public class CreateNewStoreViewModel
    {
        public string StoreEnglishName { get; set; }
        public string Name { get; set; }
        public Guid OwnerId { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
    }
}
