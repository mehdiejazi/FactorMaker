using Models.Base;
using System;

namespace Models
{
    public class Store : EntityBase
    {
        public string StoreEnglishName { get; set; }
        public string Name { get; set; }
        public User Owner { get; set; }
        public Guid OwnerId { get; set; }
        public string Url { get; set; }
        public ImageAsset Logo { get; set; }
        public Guid LogoId { get; set; }
        public string Description { get; set; }
    }
}
