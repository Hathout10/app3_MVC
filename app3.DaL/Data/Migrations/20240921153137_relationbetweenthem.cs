using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app3.DaL.Data.Migrations
{
    /// <inheritdoc />
    public partial class relationbetweenthem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "workforId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_workforId",
                table: "Employees",
                column: "workforId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_workforId",
                table: "Employees",
                column: "workforId",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_workforId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_workforId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "workforId",
                table: "Employees");
        }
    }
}
