using System.Text.Json.Serialization;

namespace SalePurchasesys.Models
{
    public class SaleDetail
    {
        public int Id { get; set; }

        // Foreign Key for Sale
        public int SaleId { get; set; }

        [JsonIgnore]  // Navigation Property for Sale
        public Sale? Sale { get; set; }

        public int ProductId { get; set; }

        [JsonIgnore]  // Navigation Property for Product
        public Product? Product { get; set; }

        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }

        // If the Subtotal is to be calculated dynamically, use this property:
        public void CalculateSubtotal(decimal price)
        {
            TotalAmount = Quantity * price;
        }
    }
}
