using Models.Base;
using System;

namespace Models
{
    public class FactorItem : EntityBase
    {
        public Factor Factor { get; set; }
        public Guid FactorId { get; set; }
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public byte OffPercent { get; set; }
        public decimal Price { get; set; }
    }
}