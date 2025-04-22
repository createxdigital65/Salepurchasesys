using SalePurchasesys.Models;
using SalePurchasesys.Services;
using System.Text.Json.Serialization;

namespace SalePurchasesys.Models
{
    public class PurchaseDetail
    {
        public int Id { get; set; }

        public int PurchaseId { get; set; }
        [JsonIgnore]
        public Purchase? Purchase { get; set; }

        public int ProductId { get; set; }
        [JsonIgnore]
        public Product? Product { get; set; }

        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
