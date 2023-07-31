using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Football_Fantasy.Migrations
{
    /// <inheritdoc />
    public partial class TenMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Teamprimary_key",
                table: "Players",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PlayerPosition",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    position = table.Column<string>(type: "TEXT", nullable: false),
                    Teamprimary_key = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerPosition", x => x.id);
                    table.ForeignKey(
                        name: "FK_PlayerPosition_Teams_Teamprimary_key",
                        column: x => x.Teamprimary_key,
                        principalTable: "Teams",
                        principalColumn: "primary_key");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_Teamprimary_key",
                table: "Players",
                column: "Teamprimary_key");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerPosition_Teamprimary_key",
                table: "PlayerPosition",
                column: "Teamprimary_key");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Teams_Teamprimary_key",
                table: "Players",
                column: "Teamprimary_key",
                principalTable: "Teams",
                principalColumn: "primary_key");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Teams_Teamprimary_key",
                table: "Players");

            migrationBuilder.DropTable(
                name: "PlayerPosition");

            migrationBuilder.DropIndex(
                name: "IX_Players_Teamprimary_key",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Teamprimary_key",
                table: "Players");
        }
    }
}
