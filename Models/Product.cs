using Models.Base;

namespace Models
{
    public class Product : EntityBase
    {
        public User Owner;
        public string Name { get; set; }
        public long Price { get; set; }
        public Category Category { get; set; }
    }
}
