using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ReservationContractMigrationNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CVV",
                schema: "Data",
                table: "ReservationContracts",
                type: "int",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardDateExpiration",
                schema: "Data",
                table: "ReservationContracts",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardName",
                schema: "Data",
                table: "ReservationContracts",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardNumber",
                schema: "Data",
                table: "ReservationContracts",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardType",
                schema: "Data",
                table: "ReservationContracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardYearExpiration",
                schema: "Data",
                table: "ReservationContracts",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DriverFirstName",
                schema: "Data",
                table: "ReservationContracts",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DriverIdentificationNumber",
                schema: "Data",
                table: "ReservationContracts",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DriverLastName",
                schema: "Data",
                table: "ReservationContracts",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DriverLicenceNumber",
                schema: "Data",
                table: "ReservationContracts",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                schema: "Data",
                table: "ReservationContracts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CVV",
                schema: "Data",
                table: "ReservationContracts");

            migrationBuilder.DropColumn(
                name: "CardDateExpiration",
                schema: "Data",
                table: "ReservationContracts");

            migrationBuilder.DropColumn(
                name: "CardName",
                schema: "Data",
                table: "ReservationContracts");

            migrationBuilder.DropColumn(
                name: "CardNumber",
                schema: "Data",
                table: "ReservationContracts");

            migrationBuilder.DropColumn(
                name: "CardType",
                schema: "Data",
                table: "ReservationContracts");

            migrationBuilder.DropColumn(
                name: "CardYearExpiration",
                schema: "Data",
                table: "ReservationContracts");

            migrationBuilder.DropColumn(
                name: "DriverFirstName",
                schema: "Data",
                table: "ReservationContracts");

            migrationBuilder.DropColumn(
                name: "DriverIdentificationNumber",
                schema: "Data",
                table: "ReservationContracts");

            migrationBuilder.DropColumn(
                name: "DriverLastName",
                schema: "Data",
                table: "ReservationContracts");

            migrationBuilder.DropColumn(
                name: "DriverLicenceNumber",
                schema: "Data",
                table: "ReservationContracts");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                schema: "Data",
                table: "ReservationContracts");
        }
    }
}
