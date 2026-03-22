using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChurchAttendanceApp.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUniqueName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Members_Name",
                table: "Members");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Members_Name",
                table: "Members",
                column: "Name",
                unique: true);
        }
    }
}
