using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Football_Fantasy.Migrations
{
    /// <inheritdoc />
    public partial class TwelveMigration : Migration
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
                name: "TeamPlayer",
                columns: table => new
                {
                    primary_key = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    code = table.Column<string>(type: "TEXT", nullable: false),
                    element_type = table.Column<string>(type: "TEXT", nullable: false),
                    first_name = table.Column<string>(type: "TEXT", nullable: false),
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    now_cost = table.Column<string>(type: "TEXT", nullable: false),
                    photo = table.Column<string>(type: "TEXT", nullable: false),
                    second_name = table.Column<string>(type: "TEXT", nullable: false),
                    team = table.Column<string>(type: "TEXT", nullable: false),
                    total_points = table.Column<string>(type: "TEXT", nullable: false),
                    web_name = table.Column<string>(type: "TEXT", nullable: false),
                    Teamprimary_key = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPlayer", x => x.primary_key);
                    table.ForeignKey(
                        name: "FK_TeamPlayer_Teams_Teamprimary_key",
                        column: x => x.Teamprimary_key,
                        principalTable: "Teams",
                        principalColumn: "primary_key");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayer_Teamprimary_key",
                table: "TeamPlayer",
                column: "Teamprimary_key");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamPlayer");

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
