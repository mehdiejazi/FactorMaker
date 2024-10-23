using Models.Base;
using System;

namespace Models
{
    public class Customer : PersonBase
    {
        public Store Store { get; set; }
        public Guid StoreId { get; set; }
    }

}
