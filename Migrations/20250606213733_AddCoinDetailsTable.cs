using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoScopeAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCoinDetailsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoinDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CoinId = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Symbol = table.Column<string>(type: "TEXT", nullable: false),
                    ImageThumb = table.Column<string>(type: "TEXT", nullable: false),
                    ImageSmall = table.Column<string>(type: "TEXT", nullable: false),
                    ImageLarge = table.Column<string>(type: "TEXT", nullable: false),
                    CurrentPriceUsd = table.Column<decimal>(type: "TEXT", nullable: false),
                    MarketCapUsd = table.Column<decimal>(type: "TEXT", nullable: false),
                    PriceChangePercentage24h = table.Column<double>(type: "REAL", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinDetails", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoinDetails");
        }
    }
}
