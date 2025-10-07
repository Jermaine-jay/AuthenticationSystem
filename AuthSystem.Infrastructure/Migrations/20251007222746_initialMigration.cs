using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthSystem.Infrastructure.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserInRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsBanned = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserTypeId = table.Column<int>(type: "int", nullable: false),
                    LoginFailedCount = table.Column<int>(type: "int", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPermission",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPermission_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPermission_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, false, "GetAllMinisters" },
                    { 2, false, "GetAllUsers" },
                    { 3, false, "GetSingleUser" },
                    { 4, false, "BlockUser" },
                    { 5, false, "CreateRole" },
                    { 6, false, "UsersInRole" },
                    { 7, false, "AddUserToRole" },
                    { 8, false, "ViewAllPermission" },
                    { 9, false, "RemoveUserFromRole" },
                    { 10, false, "ViewAllRoles" },
                    { 11, false, "RemoveRole" },
                    { 12, false, "AddUserPermission" },
                    { 13, false, "RemoveUserPermission" },
                    { 14, false, "UnblockUser" }
                });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "Id", "Active", "DateCreated", "DateDeleted", "IsDeleted", "LastModified", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { "0a4bf446-cace-4cbb-a691-b39cadecd15d", true, new DateTime(2025, 10, 7, 22, 27, 46, 608, DateTimeKind.Utc).AddTicks(122), null, false, new DateTime(2025, 10, 7, 22, 27, 46, 608, DateTimeKind.Utc).AddTicks(122), 5, "6ec0c60b-1219-4417-ab71-ab1e354f9082" },
                    { "0d0db555-10f7-482e-a6b8-8b2c49c0546d", true, new DateTime(2025, 10, 7, 22, 27, 46, 608, DateTimeKind.Utc).AddTicks(112), null, false, new DateTime(2025, 10, 7, 22, 27, 46, 608, DateTimeKind.Utc).AddTicks(113), 3, "6ec0c60b-1219-4417-ab71-ab1e354f9082" },
                    { "1aac6b44-3943-497e-a12e-ccb6ddcc7c63", true, new DateTime(2025, 10, 7, 22, 27, 46, 608, DateTimeKind.Utc).AddTicks(164), null, false, new DateTime(2025, 10, 7, 22, 27, 46, 608, DateTimeKind.Utc).AddTicks(165), 13, "6ec0c60b-1219-4417-ab71-ab1e354f9082" },
                    { "211bd871-4ef4-475a-b07c-9d86ba6a7a7c", true, new DateTime(2025, 10, 7, 22, 27, 46, 608, DateTimeKind.Utc).AddTicks(117), null, false, new DateTime(2025, 10, 7, 22, 27, 46, 608, DateTimeKind.Utc).AddTicks(118), 4, "6ec0c60b-1219-4417-ab71-ab1e354f9082" },
                    { "3bf2e9a2-9449-47e2-8771-6bf8b81c13e2", true, new DateTime(2025, 10, 7, 22, 27, 46, 608, DateTimeKind.Utc).AddTicks(168), null, false, new DateTime(2025, 10, 7, 22, 27, 46, 608, DateTimeKind.Utc).AddTicks(169), 14, "6ec0c60b-1219-4417-ab71-ab1e354f9082" },
                    { "428c5b20-bcb1-49f8-95d6-2b5182559375", true, new DateTime(2025, 10, 7, 22, 27, 46, 608, DateTimeKind.Utc).AddTicks(108), null, false, new DateTime(2025, 10, 7, 22, 27, 46, 608, DateTimeKind.Utc).AddTicks(108), 2, "6ec0c60b-1219-4417-ab71-ab1e354f9082" },
                    { "476f3c93-57d9-4ff5-b5e6-91c0e1269f13", true, new DateTime(2025, 10, 7, 22, 27, 46, 608, DateTimeKind.Utc).AddTicks(137), null, false, new DateTime(2025, 10, 7, 22, 27, 46, 608, DateTimeKind.Utc).AddTicks(137), 7, "6ec0c60b-1219-4417-ab71-ab1e354f9082" },
                    { "4c7bf70a-44e3-4b4a-9703-0826a231bc38", true, new DateTime(2025, 10, 7, 22, 27, 46, 608, DateTimeKind.Utc).AddTicks(128), null, false, new DateTime(2025, 10, 7, 22, 27, 46, 608, DateTimeKind.Utc).AddTicks(128), 6, "6ec0c60b-1219-4417-ab71-ab1e354f9082" },
                    { "64800dee-e773-475a-9959-d3cd6048129a", true, new DateTime(2025, 10, 7, 22, 27, 46, 608, DateTimeKind.Utc).AddTicks(141), null, false, new DateTime(2025, 10, 7, 22, 27, 46, 608, DateTimeKind.Utc).AddTicks(141), 8, "6ec0c60b-1219-4417-ab71-ab1e354f9082" },
                    { "bffafab8-cbec-4cf0-b7de-1c3d1cea61c7", true, new DateTime(2025, 10, 7, 22, 27, 46, 608, DateTimeKind.Utc).AddTicks(155), null, false, new DateTime(2025, 10, 7, 22, 27, 46, 608, DateTimeKind.Utc).AddTicks(156), 11, "6ec0c60b-1219-4417-ab71-ab1e354f9082" },
                    { "c12e40a0-8b50-42e4-8cee-81b96df52667", true, new DateTime(2025, 10, 7, 22, 27, 46, 608, DateTimeKind.Utc).AddTicks(145), null, false, new DateTime(2025, 10, 7, 22, 27, 46, 608, DateTimeKind.Utc).AddTicks(146), 9, "6ec0c60b-1219-4417-ab71-ab1e354f9082" },
                    { "e244ed59-982e-45ec-8859-ef6ca7a8f920", true, new DateTime(2025, 10, 7, 22, 27, 46, 608, DateTimeKind.Utc).AddTicks(160), null, false, new DateTime(2025, 10, 7, 22, 27, 46, 608, DateTimeKind.Utc).AddTicks(160), 12, "6ec0c60b-1219-4417-ab71-ab1e354f9082" },
                    { "edbf484f-5cef-4187-b403-61476a2e7712", true, new DateTime(2025, 10, 7, 22, 27, 46, 608, DateTimeKind.Utc).AddTicks(102), null, false, new DateTime(2025, 10, 7, 22, 27, 46, 608, DateTimeKind.Utc).AddTicks(102), 1, "6ec0c60b-1219-4417-ab71-ab1e354f9082" },
                    { "f83ec0dc-b5d8-4c4e-97e8-16ec99adc045", true, new DateTime(2025, 10, 7, 22, 27, 46, 608, DateTimeKind.Utc).AddTicks(151), null, false, new DateTime(2025, 10, 7, 22, 27, 46, 608, DateTimeKind.Utc).AddTicks(151), 10, "6ec0c60b-1219-4417-ab71-ab1e354f9082" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Active", "DateCreated", "DateDeleted", "IsDeleted", "LastModified", "Name" },
                values: new object[,]
                {
                    { "0006fc23-5048-4cb2-8b55-fe9f9f33606d", true, new DateTime(2025, 10, 7, 22, 27, 46, 607, DateTimeKind.Utc).AddTicks(8140), null, false, new DateTime(2025, 10, 7, 22, 27, 46, 607, DateTimeKind.Utc).AddTicks(8140), "Admin" },
                    { "1b219c2e-0945-4605-8451-44ac780d4bfe", true, new DateTime(2025, 10, 7, 22, 27, 46, 607, DateTimeKind.Utc).AddTicks(8148), null, false, new DateTime(2025, 10, 7, 22, 27, 46, 607, DateTimeKind.Utc).AddTicks(8148), "User" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Name",
                table: "Permissions",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermission_PermissionId",
                table: "UserPermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermission_UserId",
                table: "UserPermission",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users",
                column: "PhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserTypeId",
                table: "Users",
                column: "UserTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserInRoles");

            migrationBuilder.DropTable(
                name: "UserPermission");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
