using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CarCategoryMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarModel_CarBrands_CarBrandId",
                table: "CarModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarModel",
                table: "CarModel");

            migrationBuilder.RenameTable(
                name: "CarModel",
                newName: "CarModels",
                newSchema: "Catalog");

            migrationBuilder.RenameIndex(
                name: "IX_CarModel_CarBrandId",
                schema: "Catalog",
                table: "CarModels",
                newName: "IX_CarModels_CarBrandId");

            migrationBuilder.AlterColumn<string>(
                name: "CarModelName",
                schema: "Catalog",
                table: "CarModels",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarModels",
                schema: "Catalog",
                table: "CarModels",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CarCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarCategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarModels_CarCategoryId",
                schema: "Catalog",
                table: "CarModels",
                column: "CarCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarModels_CarBrands_CarBrandId",
                schema: "Catalog",
                table: "CarModels",
                column: "CarBrandId",
                principalSchema: "Catalog",
                principalTable: "CarBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarModels_CarCategory_CarCategoryId",
                schema: "Catalog",
                table: "CarModels",
                column: "CarCategoryId",
                principalTable: "CarCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarModels_CarBrands_CarBrandId",
                schema: "Catalog",
                table: "CarModels");

            migrationBuilder.DropForeignKey(
                name: "FK_CarModels_CarCategory_CarCategoryId",
                schema: "Catalog",
                table: "CarModels");

            migrationBuilder.DropTable(
                name: "CarCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarModels",
                schema: "Catalog",
                table: "CarModels");

            migrationBuilder.DropIndex(
                name: "IX_CarModels_CarCategoryId",
                schema: "Catalog",
                table: "CarModels");

            migrationBuilder.RenameTable(
                name: "CarModels",
                schema: "Catalog",
                newName: "CarModel");

            migrationBuilder.RenameIndex(
                name: "IX_CarModels_CarBrandId",
                table: "CarModel",
                newName: "IX_CarModel_CarBrandId");

            migrationBuilder.AlterColumn<string>(
                name: "CarModelName",
                table: "CarModel",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarModel",
                table: "CarModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarModel_CarBrands_CarBrandId",
                table: "CarModel",
                column: "CarBrandId",
                principalSchema: "Catalog",
                principalTable: "CarBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
