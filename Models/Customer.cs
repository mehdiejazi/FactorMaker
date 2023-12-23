using Models.Base;

namespace Models
{
    public class Customer : PersonBase
    {
        public User Owner { get; set; }
    }

}
