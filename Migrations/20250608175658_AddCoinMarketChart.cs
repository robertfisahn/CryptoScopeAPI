using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoScopeAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCoinMarketChart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoinMarketCharts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CoinId = table.Column<string>(type: "TEXT", nullable: false),
                    TimeRange = table.Column<string>(type: "TEXT", nullable: false),
                    PricesJson = table.Column<string>(type: "TEXT", nullable: false),
                    MarketCapsJson = table.Column<string>(type: "TEXT", nullable: false),
                    TotalVolumesJson = table.Column<string>(type: "TEXT", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinMarketCharts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoinMarketCharts");
        }
    }
}
