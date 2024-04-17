using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgiletyFramework.DBModels.Migrations
{
    /// <inheritdoc />
    public partial class turnMenuEntity_columnMenuTesttoMenuText : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MenuTest",
                table: "Menu",
                newName: "MenuText");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MenuText",
                table: "Menu",
                newName: "MenuTest");
        }
    }
}
