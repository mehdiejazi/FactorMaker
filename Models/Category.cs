using Models.Base;

namespace Models
{
    public class Category : EntityBase
    {
        public User Owner { get; set; }
        public string Name { get; set; }
    }
}
