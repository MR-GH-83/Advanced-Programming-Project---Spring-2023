using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Football_Fantasy.Migrations
{
    /// <inheritdoc />
    public partial class ForthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Teams_Teamprimary_key",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_Teamprimary_key",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Teamprimary_key",
                table: "Players");

            migrationBuilder.CreateTable(
                name: "RealTeams",
                columns: table => new
                {
                    primary_key = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    id = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealTeams", x => x.primary_key);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RealTeams");

            migrationBuilder.AddColumn<int>(
                name: "Teamprimary_key",
                table: "Players",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_Teamprimary_key",
                table: "Players",
                column: "Teamprimary_key");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Teams_Teamprimary_key",
                table: "Players",
                column: "Teamprimary_key",
                principalTable: "Teams",
                principalColumn: "primary_key");
        }
    }
}
