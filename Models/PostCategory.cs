using Models.Base;
using System;

namespace Models
{
    public class PostCategory : EntityBase
    {
        public PostCategory() : base()
        {

        }
        public string Name { get; set; }
        public virtual User Owner { get; set; }
        public Guid OwnerId { get; set; }
    }
}
