using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PimIVBackend.Migrations
{
    public partial class AddingFolioAndFolioItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FolioId",
                table: "Entity",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Folios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationId = table.Column<int>(type: "int", nullable: true),
                    FolioStatus = table.Column<int>(type: "int", nullable: false),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Folios_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FolioItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    EntityGuestId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FolioId = table.Column<int>(type: "int", nullable: true),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolioItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FolioItems_Entity_EntityGuestId",
                        column: x => x.EntityGuestId,
                        principalTable: "Entity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FolioItems_Folios_FolioId",
                        column: x => x.FolioId,
                        principalTable: "Folios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FolioItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entity_FolioId",
                table: "Entity",
                column: "FolioId");

            migrationBuilder.CreateIndex(
                name: "IX_FolioItems_EntityGuestId",
                table: "FolioItems",
                column: "EntityGuestId");

            migrationBuilder.CreateIndex(
                name: "IX_FolioItems_FolioId",
                table: "FolioItems",
                column: "FolioId");

            migrationBuilder.CreateIndex(
                name: "IX_FolioItems_ProductId",
                table: "FolioItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Folios_ReservationId",
                table: "Folios",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entity_Folios_FolioId",
                table: "Entity",
                column: "FolioId",
                principalTable: "Folios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entity_Folios_FolioId",
                table: "Entity");

            migrationBuilder.DropTable(
                name: "FolioItems");

            migrationBuilder.DropTable(
                name: "Folios");

            migrationBuilder.DropIndex(
                name: "IX_Entity_FolioId",
                table: "Entity");

            migrationBuilder.DropColumn(
                name: "FolioId",
                table: "Entity");
        }
    }
}
