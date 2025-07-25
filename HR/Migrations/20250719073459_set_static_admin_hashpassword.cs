using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.Migrations
{
    /// <inheritdoc />
    public partial class set_static_admin_hashpassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "HashedPassword",
                value: "$2a$11$qc5hdfdYOdR2AOrF33a / N.W9BbiyLcf1n6VZjJPqAxJUE5d8wqByG");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "HashedPassword",
                value: "$2a$11$qc5hdfdYOdR2AOrF33a/N.W9BbiyLcf1n6VZjJPqAxJUE5d8wqByG");
        }
    }
}
