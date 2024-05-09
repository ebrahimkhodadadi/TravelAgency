using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelAgency.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Master");

            migrationBuilder.EnsureSchema(
                name: "Outbox");

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "Master",
                columns: table => new
                {
                    Id = table.Column<string>(type: "Char(26)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<string>(type: "VarChar(6)", nullable: false),
                    Rank = table.Column<string>(type: "VarChar(8)", nullable: false, defaultValue: "Standard"),
                    DateOfBirth = table.Column<DateTimeOffset>(type: "DateTimeOffset(2)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "DateTimeOffset(2)", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: true),
                    CreatedBy = table.Column<string>(type: "VarChar(30)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "VarChar(30)", nullable: true),
                    UserId = table.Column<string>(type: "Char(26)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessage",
                schema: "Outbox",
                columns: table => new
                {
                    Id = table.Column<string>(type: "Char(26)", nullable: false),
                    Type = table.Column<string>(type: "VarChar(100)", nullable: false),
                    Content = table.Column<string>(type: "VarChar(5000)", nullable: false),
                    OccurredOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ExecutionStatus = table.Column<string>(type: "VarChar(10)", nullable: false, defaultValue: "InProgress"),
                    AttemptCount = table.Column<int>(type: "int", nullable: false),
                    NextProcessAttempt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ProcessedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Error = table.Column<string>(type: "VarChar(8000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessageConsumer",
                schema: "Outbox",
                columns: table => new
                {
                    Id = table.Column<string>(type: "Char(26)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessageConsumer", x => new { x.Id, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                schema: "Master",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "TinyInt", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VarChar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "Master",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "TinyInt", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VarChar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                schema: "Master",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Building = table.Column<int>(type: "int", maxLength: 1000, nullable: false),
                    Flat = table.Column<int>(type: "int", maxLength: 1000, nullable: true),
                    CustomerId = table.Column<string>(type: "Char(26)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Master",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Master",
                columns: table => new
                {
                    Id = table.Column<string>(type: "Char(26)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "DateTimeOffset(2)", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: true),
                    CreatedBy = table.Column<string>(type: "VarChar(30)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "VarChar(30)", nullable: true),
                    PasswordHash = table.Column<string>(type: "NChar(514)", nullable: false),
                    CustomerId = table.Column<string>(type: "Char(26)", nullable: true),
                    RefreshToken = table.Column<string>(type: "VarChar(32)", nullable: true),
                    TwoFactorTokenHash = table.Column<string>(type: "NChar(514)", nullable: true),
                    TwoFactorToptSecret = table.Column<string>(type: "Char(32)", nullable: true),
                    TwoFactorTokenCreatedOn = table.Column<DateTimeOffset>(type: "DateTimeOffset(2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Master",
                        principalTable: "Customer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                schema: "Master",
                columns: table => new
                {
                    RoleId = table.Column<byte>(type: "TinyInt", nullable: false),
                    PermissionId = table.Column<byte>(type: "TinyInt", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "Master",
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Master",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleUser",
                schema: "Master",
                columns: table => new
                {
                    RoleId = table.Column<byte>(type: "TinyInt", nullable: false),
                    UserId = table.Column<string>(type: "Char(26)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_RoleUser_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Master",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Master",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Master",
                table: "Permission",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (byte)1, "Review_Read" },
                    { (byte)2, "Review_Add" },
                    { (byte)3, "Review_Update" },
                    { (byte)4, "Review_Remove" },
                    { (byte)5, "INVALID_PERMISSION" }
                });

            migrationBuilder.InsertData(
                schema: "Master",
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (byte)1, "Customer" },
                    { (byte)2, "Employee" },
                    { (byte)3, "Manager" },
                    { (byte)4, "Administrator" }
                });

            migrationBuilder.InsertData(
                schema: "Master",
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { (byte)1, (byte)1 },
                    { (byte)2, (byte)1 },
                    { (byte)3, (byte)1 },
                    { (byte)1, (byte)2 },
                    { (byte)1, (byte)3 },
                    { (byte)2, (byte)3 },
                    { (byte)3, (byte)3 },
                    { (byte)1, (byte)4 },
                    { (byte)2, (byte)4 },
                    { (byte)3, (byte)4 },
                    { (byte)4, (byte)4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_CustomerId",
                schema: "Master",
                table: "Address",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessage_ExecutionStatus",
                schema: "Outbox",
                table: "OutboxMessage",
                column: "ExecutionStatus",
                filter: "[ExecutionStatus] = 'InProgress'");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_PermissionId",
                schema: "Master",
                table: "RolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UserId",
                schema: "Master",
                table: "RoleUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_CustomerId",
                schema: "Master",
                table: "User",
                column: "CustomerId",
                unique: true,
                filter: "[CustomerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UX_User_Email",
                schema: "Master",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_Username_Email",
                schema: "Master",
                table: "User",
                column: "Username",
                unique: true)
                .Annotation("SqlServer:Include", new[] { "Email" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "OutboxMessage",
                schema: "Outbox");

            migrationBuilder.DropTable(
                name: "OutboxMessageConsumer",
                schema: "Outbox");

            migrationBuilder.DropTable(
                name: "RolePermission",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "RoleUser",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "Permission",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "Master");
        }
    }
}
