using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CarCategoryMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarModels_CarCategory_CarCategoryId",
                schema: "Catalog",
                table: "CarModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarCategory",
                table: "CarCategory");

            migrationBuilder.RenameTable(
                name: "CarCategory",
                newName: "CarCategories",
                newSchema: "Catalog");

            migrationBuilder.AlterColumn<string>(
                name: "ShortName",
                schema: "Catalog",
                table: "CarCategories",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Catalog",
                table: "CarCategories",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "Catalog",
                table: "CarCategories",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarCategories",
                schema: "Catalog",
                table: "CarCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarModels_CarCategories_CarCategoryId",
                schema: "Catalog",
                table: "CarModels",
                column: "CarCategoryId",
                principalSchema: "Catalog",
                principalTable: "CarCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarModels_CarCategories_CarCategoryId",
                schema: "Catalog",
                table: "CarModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarCategories",
                schema: "Catalog",
                table: "CarCategories");

            migrationBuilder.RenameTable(
                name: "CarCategories",
                schema: "Catalog",
                newName: "CarCategory");

            migrationBuilder.AlterColumn<string>(
                name: "ShortName",
                table: "CarCategory",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CarCategory",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CarCategory",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarCategory",
                table: "CarCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarModels_CarCategory_CarCategoryId",
                schema: "Catalog",
                table: "CarModels",
                column: "CarCategoryId",
                principalTable: "CarCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
