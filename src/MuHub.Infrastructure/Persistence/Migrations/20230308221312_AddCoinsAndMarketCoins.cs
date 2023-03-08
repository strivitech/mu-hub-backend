using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MuHub.Infrastructure.Persistence.Migrations
{
    public partial class AddCoinsAndMarketCoins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExternalSymbolId = table.Column<string>(type: "text", nullable: false),
                    SymbolId = table.Column<string>(type: "text", nullable: false),
                    Symbol = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarketCoins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SymbolId = table.Column<string>(type: "text", nullable: false),
                    Symbol = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    CurrentPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    MarketCapRank = table.Column<int>(type: "integer", nullable: true),
                    High24H = table.Column<decimal>(type: "numeric", nullable: true),
                    Low24H = table.Column<decimal>(type: "numeric", nullable: true),
                    LastUpdated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsValid = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketCoins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarketCoinsUpdateTimestamps",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LastUpdated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketCoinsUpdateTimestamps", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coins");

            migrationBuilder.DropTable(
                name: "MarketCoins");

            migrationBuilder.DropTable(
                name: "MarketCoinsUpdateTimestamps");
        }
    }
}
