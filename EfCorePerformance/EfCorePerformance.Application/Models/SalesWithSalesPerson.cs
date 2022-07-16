namespace EfCorePerformance.Application.Models
{
    public class SalesWithSalesPerson
    {
        public int SalesId { get; set; }
        public int SalesPersonId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public string SalesPersonFirstName { get; set; }
        public string SalesPersonLastName { get; set; }
    }
}
