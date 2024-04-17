using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgiletyFramework.DBModels.Migrations
{
    /// <inheritdoc />
    public partial class addcolumnQQinUserEntity327 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QQ",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QQ",
                table: "User");
        }
    }
}
