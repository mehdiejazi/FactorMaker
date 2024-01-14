using System;

namespace Data.DataTransferObjects.Factor
{
    public class FactorSaleWeekDailyViewModel
    {
        public DayOfWeek DayOfWeek { get;  set; }
        public int Count { get;  set; }
        public decimal TotalPrice { get;  set; }
    }
}
