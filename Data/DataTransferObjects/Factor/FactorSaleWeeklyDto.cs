namespace Data.DataTransferObjects.Factor
{
    public class FactorSaleWeeklyDto
    {
        public int Year { get; set; }
        public int Week { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
