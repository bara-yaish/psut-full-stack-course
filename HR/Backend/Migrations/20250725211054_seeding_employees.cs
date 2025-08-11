using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HR.Migrations
{
    /// <inheritdoc />
    public partial class seeding_employees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BirthDate", "DepartmentId", "EndDate", "IsActive", "ManagerId", "Name", "Phone", "PositionId", "StartDate", "UserId" },
                values: new object[,]
                {
                    { 1L, null, 1L, null, true, null, "manager", null, -3L, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2L, null, 1L, null, true, null, "employee", null, -4L, new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2L);
        }
    }
}
