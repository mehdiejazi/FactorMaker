using Models.Base;
using System;

namespace Models
{
    public class StoreSocialLink : EntityBase
    {
        public Guid StoreId { get; set; }
        public Store Store { get; set; }
        public string Platform { get; set; }
        public string Url { get; set; }
        public string Label { get; set; }
        public bool IsPrimary { get; set; }
    }
}
