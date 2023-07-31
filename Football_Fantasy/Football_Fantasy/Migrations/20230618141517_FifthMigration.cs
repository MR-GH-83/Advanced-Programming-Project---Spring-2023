using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Football_Fantasy.Migrations
{
    /// <inheritdoc />
    public partial class FifthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Score",
                table: "Teams",
                newName: "score");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Teams",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "Teams",
                newName: "user_email");

            migrationBuilder.AddColumn<int>(
                name: "price",
                table: "Teams",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "score",
                table: "Teams",
                newName: "Score");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Teams",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "user_email",
                table: "Teams",
                newName: "UserEmail");
        }
    }
}
