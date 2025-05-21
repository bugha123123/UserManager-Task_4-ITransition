using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task_4.Migrations
{
    /// <inheritdoc />
    public partial class addinglastseenproptoUsermodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastLoginTime",
                table: "AspNetUsers",
                newName: "LastLeen");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastLeen",
                table: "AspNetUsers",
                newName: "LastLoginTime");
        }
    }
}
