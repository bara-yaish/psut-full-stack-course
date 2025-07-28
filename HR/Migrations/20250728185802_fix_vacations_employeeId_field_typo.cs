using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.Migrations
{
    /// <inheritdoc />
    public partial class fix_vacations_employeeId_field_typo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacations_Employees_EmplopyeeId",
                table: "Vacations");

            migrationBuilder.RenameColumn(
                name: "EmplopyeeId",
                table: "Vacations",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Vacations_EmplopyeeId",
                table: "Vacations",
                newName: "IX_Vacations_EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacations_Employees_EmployeeId",
                table: "Vacations",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacations_Employees_EmployeeId",
                table: "Vacations");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Vacations",
                newName: "EmplopyeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Vacations_EmployeeId",
                table: "Vacations",
                newName: "IX_Vacations_EmplopyeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacations_Employees_EmplopyeeId",
                table: "Vacations",
                column: "EmplopyeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
