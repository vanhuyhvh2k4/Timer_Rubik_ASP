using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timer_Rubik.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class Update_Migration_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Solves");

            migrationBuilder.AddColumn<string>(
                name: "Solve",
                table: "Scrambles",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Solve",
                table: "Scrambles");

            migrationBuilder.CreateTable(
                name: "Solves",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ScrambleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Answer = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Solves_Scrambles_ScrambleId",
                        column: x => x.ScrambleId,
                        principalTable: "Scrambles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Solves_ScrambleId",
                table: "Solves",
                column: "ScrambleId",
                unique: true);
        }
    }
}
