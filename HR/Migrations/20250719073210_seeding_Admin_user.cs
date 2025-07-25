using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HR.Migrations
{
    /// <inheritdoc />
    public partial class seeding_Admin_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "Employees");

            migrationBuilder.AddColumn<long>(
                name: "PositionId",
                table: "Employees",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TypeId",
                table: "Departments",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Lookup",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MajorCode = table.Column<int>(type: "int", nullable: false),
                    MinorCode = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lookup", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Lookup",
                columns: new[] { "Id", "MajorCode", "MinorCode", "Name" },
                values: new object[,]
                {
                    { 1L, 0, 0, "Employee Positions" },
                    { 2L, 0, 1, "HR" },
                    { 3L, 0, 2, "Manager" },
                    { 4L, 0, 3, "Developer" },
                    { 5L, 1, 0, "Department Types" },
                    { 6L, 1, 1, "Finance" },
                    { 7L, 1, 2, "Adminstrative" },
                    { 8L, 1, 3, "Technical" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "HashedPassword", "IsAdmin", "UserName" },
                values: new object[] { 1L, "$2a$11$qc5hdfdYOdR2AOrF33a/N.W9BbiyLcf1n6VZjJPqAxJUE5d8wqByG", true, "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionId",
                table: "Employees",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_TypeId",
                table: "Departments",
                column: "TypeId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Lookup_TypeId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Lookup_PositionId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Lookup");

            migrationBuilder.DropIndex(
                name: "IX_Employees_PositionId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Departments_TypeId",
                table: "Departments");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Departments");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Employees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
