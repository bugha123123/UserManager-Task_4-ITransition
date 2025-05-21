using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task_4.Migrations
{
    /// <inheritdoc />
    public partial class fixingtypoinUsermodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastLeen",
                table: "AspNetUsers",
                newName: "LastSeen");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastSeen",
                table: "AspNetUsers",
                newName: "LastLeen");
        }
    }
}
