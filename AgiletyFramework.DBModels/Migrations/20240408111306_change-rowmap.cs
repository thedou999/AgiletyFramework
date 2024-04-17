using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgiletyFramework.DBModels.Migrations
{
    /// <inheritdoc />
    public partial class changerowmap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoleMap",
                table: "UserRoleMap");

            migrationBuilder.DropIndex(
                name: "IX_UserRoleMap_UserId",
                table: "UserRoleMap");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleMenuMap",
                table: "RoleMenuMap");

            migrationBuilder.DropIndex(
                name: "IX_RoleMenuMap_RoleId",
                table: "RoleMenuMap");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserRoleMap");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RoleMenuMap");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoleMap",
                table: "UserRoleMap",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleMenuMap",
                table: "RoleMenuMap",
                columns: new[] { "RoleId", "MenuId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoleMap",
                table: "UserRoleMap");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleMenuMap",
                table: "RoleMenuMap");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserRoleMap",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RoleMenuMap",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoleMap",
                table: "UserRoleMap",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleMenuMap",
                table: "RoleMenuMap",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleMap_UserId",
                table: "UserRoleMap",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleMenuMap_RoleId",
                table: "RoleMenuMap",
                column: "RoleId");
        }
    }
}
