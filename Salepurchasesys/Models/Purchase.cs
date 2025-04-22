using SalePurchasesys.Models;
using SalePurchasesys.Services;
using System;
using System.Collections.Generic;

namespace SalePurchasesys.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        // Changed to decimal for monetary values
        public decimal TotalAmount { get; set; }

        public DateTime PurchaseDate { get; set; }

        // Initialize the PurchaseDetails collection to avoid null validation errors
        public ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();
    }
}
