using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Car1Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarModelId",
                schema: "Catalog",
                table: "Cars",
                column: "CarModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarModels_CarModelId",
                schema: "Catalog",
                table: "Cars",
                column: "CarModelId",
                principalSchema: "Catalog",
                principalTable: "CarModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarModels_CarModelId",
                schema: "Catalog",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CarModelId",
                schema: "Catalog",
                table: "Cars");
        }
    }
}
