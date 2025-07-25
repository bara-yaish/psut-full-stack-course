using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.Migrations
{
    /// <inheritdoc />
    public partial class whatever : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Lookup_TypeId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Lookup_PositionId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lookup",
                table: "Lookup");

            migrationBuilder.RenameTable(
                name: "Lookup",
                newName: "Lookups");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lookups",
                table: "Lookups",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "HashedPassword",
                value: "$2a$11$MVRXCTLgV2dEBLH931VhPOUHtGqqfZ.006p2emcvtxwyRAT90ngym");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Lookups_TypeId",
                table: "Departments",
                column: "TypeId",
                principalTable: "Lookups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Lookups_PositionId",
                table: "Employees",
                column: "PositionId",
                principalTable: "Lookups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Lookups_TypeId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Lookups_PositionId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lookups",
                table: "Lookups");

            migrationBuilder.RenameTable(
                name: "Lookups",
                newName: "Lookup");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lookup",
                table: "Lookup",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "HashedPassword",
                value: "$2a$11$qc5hdfdYOdR2AOrF33a / N.W9BbiyLcf1n6VZjJPqAxJUE5d8wqByG");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Lookup_TypeId",
                table: "Departments",
                column: "TypeId",
                principalTable: "Lookup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Lookup_PositionId",
                table: "Employees",
                column: "PositionId",
                principalTable: "Lookup",
                principalColumn: "Id");
        }
    }
}
