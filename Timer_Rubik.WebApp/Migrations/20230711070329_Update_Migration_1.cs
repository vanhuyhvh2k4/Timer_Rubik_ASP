using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timer_Rubik.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class Update_Migration_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Time",
                table: "Favorites",
                type: "float",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Time",
                table: "Favorites",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");
        }
    }
}
