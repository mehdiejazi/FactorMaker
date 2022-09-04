using Models.Base;

namespace Models
{
    public class FactorItem : EntityBase
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public byte OffPercent { get; set; }
    }
}