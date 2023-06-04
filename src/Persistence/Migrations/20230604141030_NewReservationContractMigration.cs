using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewReservationContractMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationContracts_CarModels_CarModelId",
                schema: "Data",
                table: "ReservationContracts");

            migrationBuilder.RenameColumn(
                name: "CarModelId",
                schema: "Data",
                table: "ReservationContracts",
                newName: "CarId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationContracts_CarModelId",
                schema: "Data",
                table: "ReservationContracts",
                newName: "IX_ReservationContracts_CarId");

            migrationBuilder.AlterColumn<string>(
                name: "DriverLicenceNumber",
                schema: "Data",
                table: "ReservationContracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DriverIdentificationNumber",
                schema: "Data",
                table: "ReservationContracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CardYearExpiration",
                schema: "Data",
                table: "ReservationContracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(4)",
                oldMaxLength: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CardNumber",
                schema: "Data",
                table: "ReservationContracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(16)",
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CardName",
                schema: "Data",
                table: "ReservationContracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CardDateExpiration",
                schema: "Data",
                table: "ReservationContracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ContractMade",
                schema: "Data",
                table: "ReservationContracts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "Data",
                table: "ReservationContracts",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Cars",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberPlate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kilometers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    FuelType = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationContracts_Cars_CarId",
                schema: "Data",
                table: "ReservationContracts",
                column: "CarId",
                principalSchema: "Catalog",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationContracts_Cars_CarId",
                schema: "Data",
                table: "ReservationContracts");

            migrationBuilder.DropTable(
                name: "Cars",
                schema: "Catalog");

            migrationBuilder.DropColumn(
                name: "ContractMade",
                schema: "Data",
                table: "ReservationContracts");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "Data",
                table: "ReservationContracts");

            migrationBuilder.RenameColumn(
                name: "CarId",
                schema: "Data",
                table: "ReservationContracts",
                newName: "CarModelId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationContracts_CarId",
                schema: "Data",
                table: "ReservationContracts",
                newName: "IX_ReservationContracts_CarModelId");

            migrationBuilder.AlterColumn<string>(
                name: "DriverLicenceNumber",
                schema: "Data",
                table: "ReservationContracts",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DriverIdentificationNumber",
                schema: "Data",
                table: "ReservationContracts",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CardYearExpiration",
                schema: "Data",
                table: "ReservationContracts",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CardNumber",
                schema: "Data",
                table: "ReservationContracts",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CardName",
                schema: "Data",
                table: "ReservationContracts",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CardDateExpiration",
                schema: "Data",
                table: "ReservationContracts",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationContracts_CarModels_CarModelId",
                schema: "Data",
                table: "ReservationContracts",
                column: "CarModelId",
                principalSchema: "Catalog",
                principalTable: "CarModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
