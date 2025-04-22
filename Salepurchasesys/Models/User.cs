using SalePurchasesys.Models;
using SalePurchasesys.Services;

namespace SalePurchasesys.Models

{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }

    }
}
