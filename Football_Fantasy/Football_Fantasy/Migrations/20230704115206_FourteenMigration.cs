using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Football_Fantasy.Migrations
{
    /// <inheritdoc />
    public partial class FourteenMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerPosition");

            migrationBuilder.DropTable(
                name: "TeamPlayer");

            migrationBuilder.CreateTable(
                name: "PlayersTeam",
                columns: table => new
                {
                    primary_key = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    playerprimary_key = table.Column<int>(type: "INTEGER", nullable: false),
                    player_position = table.Column<string>(type: "TEXT", nullable: false),
                    user_email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayersTeam", x => x.primary_key);
                    table.ForeignKey(
                        name: "FK_PlayersTeam_Players_playerprimary_key",
                        column: x => x.playerprimary_key,
                        principalTable: "Players",
                        principalColumn: "primary_key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayersTeam_playerprimary_key",
                table: "PlayersTeam",
                column: "playerprimary_key");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayersTeam");

            migrationBuilder.CreateTable(
                name: "PlayerPosition",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Teamprimary_key = table.Column<int>(type: "INTEGER", nullable: true),
                    position = table.Column<string>(type: "TEXT", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "TeamPlayer",
                columns: table => new
                {
                    primary_key = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Teamprimary_key = table.Column<int>(type: "INTEGER", nullable: true),
                    code = table.Column<string>(type: "TEXT", nullable: false),
                    element_type = table.Column<string>(type: "TEXT", nullable: false),
                    first_name = table.Column<string>(type: "TEXT", nullable: false),
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    now_cost = table.Column<string>(type: "TEXT", nullable: false),
                    photo = table.Column<string>(type: "TEXT", nullable: false),
                    second_name = table.Column<string>(type: "TEXT", nullable: false),
                    team = table.Column<string>(type: "TEXT", nullable: false),
                    total_points = table.Column<string>(type: "TEXT", nullable: false),
                    web_name = table.Column<string>(type: "TEXT", nullable: false)
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
                name: "IX_PlayerPosition_Teamprimary_key",
                table: "PlayerPosition",
                column: "Teamprimary_key");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayer_Teamprimary_key",
                table: "TeamPlayer",
                column: "Teamprimary_key");
        }
    }
}
