namespace SalePurchasesys.DTOs
{
    // ProductDto.cs
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int ProductSubCategoryId { get; set; }
        public int UserId { get; set; }
    }

    // ProductCreateDto.cs
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int ProductSubCategoryId { get; set; }
        public int UserId { get; set; }
    }

    // ProductUpdateDto.cs
    public class ProductUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int ProductSubCategoryId { get; set; }
        public int UserId { get; set; }
    }

}
