using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Football_Fantasy.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    primary_key = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserEmail = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    Score = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.primary_key);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    primary_key = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    code = table.Column<string>(type: "TEXT", nullable: false),
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
                    table.PrimaryKey("PK_Players", x => x.primary_key);
                    table.ForeignKey(
                        name: "FK_Players_Teams_Teamprimary_key",
                        column: x => x.Teamprimary_key,
                        principalTable: "Teams",
                        principalColumn: "primary_key");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_Teamprimary_key",
                table: "Players",
                column: "Teamprimary_key");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    primaryKey = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    linkId = table.Column<int>(type: "INTEGER", nullable: false),
                    token = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.primaryKey);
                });
        }
    }
}
