using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgency.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DescriptionNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Travel_TravelId",
                schema: "TravelAgency",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Payment_TravelId",
                schema: "TravelAgency",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "TravelId",
                schema: "TravelAgency",
                table: "Payment");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "TravelAgency",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "DebtLimit",
                schema: "Master",
                table: "Customer",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "TravelAgency",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TravelId",
                schema: "TravelAgency",
                table: "Payment",
                type: "Char(26)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DebtLimit",
                schema: "Master",
                table: "Customer",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payment_TravelId",
                schema: "TravelAgency",
                table: "Payment",
                column: "TravelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Travel_TravelId",
                schema: "TravelAgency",
                table: "Payment",
                column: "TravelId",
                principalSchema: "TravelAgency",
                principalTable: "Travel",
                principalColumn: "Id");
        }
    }
}
