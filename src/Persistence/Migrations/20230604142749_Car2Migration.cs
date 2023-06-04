using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Car2Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Cars_ColorId",
                schema: "Catalog",
                table: "Cars",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_OfficeId",
                schema: "Catalog",
                table: "Cars",
                column: "OfficeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Colors_ColorId",
                schema: "Catalog",
                table: "Cars",
                column: "ColorId",
                principalSchema: "Catalog",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Offices_OfficeId",
                schema: "Catalog",
                table: "Cars",
                column: "OfficeId",
                principalSchema: "Catalog",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Colors_ColorId",
                schema: "Catalog",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Offices_OfficeId",
                schema: "Catalog",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_ColorId",
                schema: "Catalog",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_OfficeId",
                schema: "Catalog",
                table: "Cars");
        }
    }
}
