namespace Data.DataTransferObjects.Factor
{
    public class FactorSaleMonthlyDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
