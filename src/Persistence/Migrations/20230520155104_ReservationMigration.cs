using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ReservationMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarModels_CarCategories_CarCategoryId",
                schema: "Catalog",
                table: "CarModels");

            migrationBuilder.EnsureSchema(
                name: "Data");

            migrationBuilder.CreateTable(
                name: "Reservations",
                schema: "Data",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PickUpDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DropDownDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CarModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PickUpLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DropDownLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_CarModels_CarModelId",
                        column: x => x.CarModelId,
                        principalSchema: "Catalog",
                        principalTable: "CarModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Offices_DropDownLocationId",
                        column: x => x.DropDownLocationId,
                        principalSchema: "Catalog",
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Offices_PickUpLocationId",
                        column: x => x.PickUpLocationId,
                        principalSchema: "Catalog",
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CarModelId",
                schema: "Data",
                table: "Reservations",
                column: "CarModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_DropDownLocationId",
                schema: "Data",
                table: "Reservations",
                column: "DropDownLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_PickUpLocationId",
                schema: "Data",
                table: "Reservations",
                column: "PickUpLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarModels_CarCategories_CarCategoryId",
                schema: "Catalog",
                table: "CarModels",
                column: "CarCategoryId",
                principalSchema: "Catalog",
                principalTable: "CarCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarModels_CarCategories_CarCategoryId",
                schema: "Catalog",
                table: "CarModels");

            migrationBuilder.DropTable(
                name: "Reservations",
                schema: "Data");

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
    }
}
