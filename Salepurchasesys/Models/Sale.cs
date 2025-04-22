namespace SalePurchasesys.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public DateTime OrderDate { get; set; }
        public int TotalAmount { get; set; }
        public DateTime Status { get; set; }

        // Initialize SaleDetails to avoid null reference errors
        public ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
       
    }
}
