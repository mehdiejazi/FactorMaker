namespace Data.DataTransferObjects.Customer
{
    public class CustomerSaleTotalPriceDto
    {
        public Models.Customer Customer { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
