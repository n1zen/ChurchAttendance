using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChurchAttendanceApp.Migrations
{
    /// <inheritdoc />
    public partial class AddChurchOfOrigin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChurchOfOrigin",
                table: "Members",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChurchOfOrigin",
                table: "Members");
        }
    }
}
