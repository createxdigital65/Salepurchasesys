namespace SalePurchasesys.DTOs
{
    public class CreatePurchaseDto
    {
        public DateTime PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
       
    }

    public class UpdatePurchaseDto
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }

    }

    public class PurchaseDto
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
    }
}
