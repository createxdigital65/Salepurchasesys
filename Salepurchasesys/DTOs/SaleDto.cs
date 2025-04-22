namespace SalePurchasesys.DTOs
{
    public class SaleDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public List<SaleDetailDto> SaleDetails { get; set; }
    }

    public class SaleCreateDto
    {
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public List<CreateSaleDetailDto> SaleDetails { get; set; }
    }

    public class SaleUpdateDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public List<UpdateSaleDetailDto> SaleDetails { get; set; }
    }
}
