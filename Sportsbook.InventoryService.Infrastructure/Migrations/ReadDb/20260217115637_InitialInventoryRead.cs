using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sportsbook.InventoryService.Infrastructure.Migrations.ReadDb
{
    /// <inheritdoc />
    public partial class InitialInventoryRead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReadInventoryItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Sku = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadInventoryItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockMovements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Sku = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    QuantityChange = table.Column<int>(type: "integer", nullable: false),
                    OccurredAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockMovements", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReadInventoryItems_Quantity",
                table: "ReadInventoryItems",
                column: "Quantity");

            migrationBuilder.CreateIndex(
                name: "IX_ReadInventoryItems_Sku",
                table: "ReadInventoryItems",
                column: "Sku",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_OccurredAt",
                table: "StockMovements",
                column: "OccurredAt");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_Sku",
                table: "StockMovements",
                column: "Sku");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReadInventoryItems");

            migrationBuilder.DropTable(
                name: "StockMovements");
        }
    }
}
