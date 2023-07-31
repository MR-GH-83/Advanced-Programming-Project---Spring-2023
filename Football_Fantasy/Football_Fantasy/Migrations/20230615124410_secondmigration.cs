using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Football_Fantasy.Migrations
{
    /// <inheritdoc />
    public partial class secondmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "score",
                table: "Users",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    first_name = table.Column<string>(type: "TEXT", nullable: false),
                    second_name = table.Column<string>(type: "TEXT", nullable: false),
                    now_const = table.Column<double>(type: "REAL", nullable: false),
                    element_type = table.Column<int>(type: "INTEGER", nullable: false),
                    team = table.Column<string>(type: "TEXT", nullable: false),
                    web_name = table.Column<string>(type: "TEXT", nullable: false),
                    total_points = table.Column<double>(type: "REAL", nullable: false),
                    photo = table.Column<string>(type: "TEXT", nullable: false),
                    code = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UserTeams",
                columns: table => new
                {
                    userId = table.Column<string>(type: "TEXT", nullable: false),
                    players = table.Column<int>(type: "INTEGER", nullable: false),
                    countOfStriker = table.Column<int>(type: "INTEGER", nullable: false),
                    countOfDefender = table.Column<int>(type: "INTEGER", nullable: false),
                    countOfGoalkeeper = table.Column<int>(type: "INTEGER", nullable: false),
                    countOfMidfielder = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTeams", x => x.userId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "UserTeams");

            migrationBuilder.DropColumn(
                name: "score",
                table: "Users");
        }
    }
}
