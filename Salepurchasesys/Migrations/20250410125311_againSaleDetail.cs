using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Salepurchasesys.Migrations
{
    /// <inheritdoc />
    public partial class againSaleDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Subtotal",
                table: "SaleDetails",
                newName: "TotalAmount");

            migrationBuilder.RenameColumn(
                name: "CostPrice",
                table: "PurchaseDetails",
                newName: "TotalAmount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "SaleDetails",
                newName: "Subtotal");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "PurchaseDetails",
                newName: "CostPrice");
        }
    }
}
