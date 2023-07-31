using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Football_Fantasy.Migrations
{
    /// <inheritdoc />
    public partial class SixteenMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "done",
                table: "Teams",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "done",
                table: "Teams");
        }
    }
}
