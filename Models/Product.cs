using Models.Base;
using System;

namespace Models
{
    public class Product : EntityBase
    {
        public Store Store { get; set; }
        public Guid StoreId { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
        public ImageAsset Image { get; set; }
        public Guid? ImageId { get; set; }
        public string ShortDescription { get; set; }
        public bool ShowOnLanding { get; set; } = true;
        public int SortOrder { get; set; }
        public Category Category { get; set; }
        public Guid CategoryId { get; set; }
    }
}
