namespace SalePurchasesys.DTOs
{

    public class UpdatePurchaseDetailDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public int PurchaseId { get; set; }
}
public class PurchaseDetailDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal SubTotal { get; set; }
    public int PurchaseId { get; set; }
}
public class CreatePurchaseDetailDto
{
    public int PurchaseId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}

}