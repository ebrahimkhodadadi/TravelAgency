using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgency.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BillAndSubTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "TravelAgency");

            migrationBuilder.AlterColumn<string>(
                name: "Rank",
                schema: "Master",
                table: "Customer",
                type: "VarChar(6)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VarChar(8)",
                oldDefaultValue: "Standard");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                schema: "Master",
                table: "Customer",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(9)",
                oldMaxLength: 9);

            migrationBuilder.AddColumn<int>(
                name: "DebtLimit",
                schema: "Master",
                table: "Customer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Bill",
                schema: "TravelAgency",
                columns: table => new
                {
                    Id = table.Column<string>(type: "Char(26)", nullable: false),
                    BillStatus = table.Column<string>(type: "VarChar(10)", nullable: false, defaultValue: "InProgress"),
                    CustomerId = table.Column<string>(type: "Char(26)", nullable: false),
                    SoftDeletedOn = table.Column<DateTimeOffset>(type: "DateTimeOffset(2)", nullable: true),
                    SoftDeleted = table.Column<bool>(type: "Bit", nullable: false, defaultValue: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "DateTimeOffset(2)", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: true),
                    CreatedBy = table.Column<string>(type: "VarChar(30)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "VarChar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bill_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Master",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscountLog",
                schema: "TravelAgency",
                columns: table => new
                {
                    Id = table.Column<string>(type: "Char(26)", nullable: false),
                    BillId = table.Column<string>(type: "Char(26)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscountLog_Bill_BillId",
                        column: x => x.BillId,
                        principalSchema: "TravelAgency",
                        principalTable: "Bill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Travel",
                schema: "TravelAgency",
                columns: table => new
                {
                    Id = table.Column<string>(type: "Char(26)", nullable: false),
                    Direction_Origin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direction_Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Start = table.Column<DateTimeOffset>(type: "DateTimeOffset(2)", nullable: false),
                    TravelType = table.Column<string>(type: "VarChar(6)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    BillId = table.Column<string>(type: "Char(26)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Travel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Travel_Bill_BillId",
                        column: x => x.BillId,
                        principalSchema: "TravelAgency",
                        principalTable: "Bill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                schema: "TravelAgency",
                columns: table => new
                {
                    Id = table.Column<string>(type: "Char(26)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    BillId = table.Column<string>(type: "Char(26)", nullable: false),
                    TransferId = table.Column<string>(type: "Char(26)", nullable: true),
                    PaymentType = table.Column<string>(type: "VarChar(8)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TravelId = table.Column<string>(type: "Char(26)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Bill_BillId",
                        column: x => x.BillId,
                        principalSchema: "TravelAgency",
                        principalTable: "Bill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payment_Travel_TravelId",
                        column: x => x.TravelId,
                        principalSchema: "TravelAgency",
                        principalTable: "Travel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bill_CustomerId",
                schema: "TravelAgency",
                table: "Bill",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountLog_BillId",
                schema: "TravelAgency",
                table: "DiscountLog",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_BillId",
                schema: "TravelAgency",
                table: "Payment",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_TravelId",
                schema: "TravelAgency",
                table: "Payment",
                column: "TravelId");

            migrationBuilder.CreateIndex(
                name: "IX_Travel_BillId",
                schema: "TravelAgency",
                table: "Travel",
                column: "BillId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscountLog",
                schema: "TravelAgency");

            migrationBuilder.DropTable(
                name: "Payment",
                schema: "TravelAgency");

            migrationBuilder.DropTable(
                name: "Travel",
                schema: "TravelAgency");

            migrationBuilder.DropTable(
                name: "Bill",
                schema: "TravelAgency");

            migrationBuilder.DropColumn(
                name: "DebtLimit",
                schema: "Master",
                table: "Customer");

            migrationBuilder.AlterColumn<string>(
                name: "Rank",
                schema: "Master",
                table: "Customer",
                type: "VarChar(8)",
                nullable: false,
                defaultValue: "Standard",
                oldClrType: typeof(string),
                oldType: "VarChar(6)");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                schema: "Master",
                table: "Customer",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);
        }
    }
}
