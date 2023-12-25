using Models.Base;
using System;

namespace Models
{
    public class Store : EntityBase
    {
        public string StoreId { get; set; }
        public string Name { get; set; }
        public User Owner { get; set; }
        public Guid OwnerId { get; set; }
        public string Url { get; set; }
        public string LogoUrl { get; set; }
        public string Description { get; set; }
    }
}
