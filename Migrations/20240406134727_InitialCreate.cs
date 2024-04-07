using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RealTime_StockExchange.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    lastupdated = table.Column<DateTime>(nullable: false),
                    isUpdated = table.Column<bool>(nullable: false),
                    clientsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_clients_clients_clientsId",
                        column: x => x.clientsId,
                        principalTable: "clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    v = table.Column<decimal>(nullable: false),
                    vw = table.Column<decimal>(nullable: false),
                    o = table.Column<decimal>(nullable: false),
                    close_price = table.Column<decimal>(nullable: false),
                    highest_price = table.Column<decimal>(nullable: false),
                    lowest_price = table.Column<decimal>(nullable: false),
                    n = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_clients_clientsId",
                table: "clients",
                column: "clientsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "Stocks");
        }
    }
}
