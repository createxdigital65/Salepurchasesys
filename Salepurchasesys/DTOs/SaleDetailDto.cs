namespace SalePurchasesys.DTOs
{
    public class SaleDetailDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public int SaleId { get; set; }

    }

    public class CreateSaleDetailDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public int SaleId { get; set; }
    }

    public class UpdateSaleDetailDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public int SaleId { get; set; }
    }
}
