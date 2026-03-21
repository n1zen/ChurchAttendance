using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChurchAttendanceApp.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueMemberConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Members_MemberId",
                table: "Members",
                column: "MemberId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_Name",
                table: "Members",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Members_MemberId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_Name",
                table: "Members");
        }
    }
}
