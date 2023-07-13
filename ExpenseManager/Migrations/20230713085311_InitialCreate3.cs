using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseManager.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DailyExpense",
                table: "Users",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MonthlyExpense",
                table: "Users",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WeeklyExpense",
                table: "Users",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyExpense",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MonthlyExpense",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WeeklyExpense",
                table: "Users");
        }
    }
}
