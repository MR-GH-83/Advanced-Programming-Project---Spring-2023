using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Football_Fantasy.Migrations
{
    /// <inheritdoc />
    public partial class FifteenMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayersTeam_Players_playerprimary_key",
                table: "PlayersTeam");

            migrationBuilder.DropIndex(
                name: "IX_PlayersTeam_playerprimary_key",
                table: "PlayersTeam");

            migrationBuilder.DropColumn(
                name: "playerprimary_key",
                table: "PlayersTeam");

            migrationBuilder.AddColumn<string>(
                name: "web_name",
                table: "PlayersTeam",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "web_name",
                table: "PlayersTeam");

            migrationBuilder.AddColumn<int>(
                name: "playerprimary_key",
                table: "PlayersTeam",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PlayersTeam_playerprimary_key",
                table: "PlayersTeam",
                column: "playerprimary_key");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayersTeam_Players_playerprimary_key",
                table: "PlayersTeam",
                column: "playerprimary_key",
                principalTable: "Players",
                principalColumn: "primary_key",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
