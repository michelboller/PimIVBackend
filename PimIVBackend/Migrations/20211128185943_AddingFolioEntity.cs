using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PimIVBackend.Migrations
{
    public partial class AddingFolioEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entity_Folios_FolioId",
                table: "Entity");

            migrationBuilder.DropIndex(
                name: "IX_Entity_FolioId",
                table: "Entity");

            migrationBuilder.DropColumn(
                name: "FolioId",
                table: "Entity");

            migrationBuilder.CreateTable(
                name: "FolioEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FolioId = table.Column<int>(type: "int", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolioEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FolioEntity_Entity_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FolioEntity_Folios_FolioId",
                        column: x => x.FolioId,
                        principalTable: "Folios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FolioEntity_EntityId",
                table: "FolioEntity",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_FolioEntity_FolioId",
                table: "FolioEntity",
                column: "FolioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FolioEntity");

            migrationBuilder.AddColumn<int>(
                name: "FolioId",
                table: "Entity",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Entity_FolioId",
                table: "Entity",
                column: "FolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entity_Folios_FolioId",
                table: "Entity",
                column: "FolioId",
                principalTable: "Folios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
