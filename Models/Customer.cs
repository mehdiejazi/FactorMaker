using Models.Base;
using System;

namespace Models
{
    public class Customer : PersonBase
    {
        public User Owner { get; set; }
        public Guid OwnerId { get; set; }
    }

}
