using Models.Base;

namespace Models
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public long Price { get; set; }
    }
}
