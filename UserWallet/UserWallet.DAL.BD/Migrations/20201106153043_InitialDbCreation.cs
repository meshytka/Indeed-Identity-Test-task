using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace UserWallet.DAL.BD.Migrations
{
    public partial class InitialDbCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyAccount",
                columns: table => new
                {
                    CurrencyAccountId = table.Column<Guid>(nullable: false),
                    CurrencyType = table.Column<int>(nullable: false),
                    Value = table.Column<decimal>(nullable: false),
                    WalletId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyAccount", x => x.CurrencyAccountId);
                    table.ForeignKey(
                        name: "FK_CurrencyAccount_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyAccount_WalletId",
                table: "CurrencyAccount",
                column: "WalletId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyAccount");

            migrationBuilder.DropTable(
                name: "Wallets");
        }
    }
}