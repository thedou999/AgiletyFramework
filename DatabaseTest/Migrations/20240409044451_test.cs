using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseTest.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentTeacherMap_StudentEntity_StudentId",
                table: "StudentTeacherMap");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentTeacherMap_TeacherEntity_TeacherId",
                table: "StudentTeacherMap");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherEntity",
                table: "TeacherEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentEntity",
                table: "StudentEntity");

            migrationBuilder.RenameTable(
                name: "TeacherEntity",
                newName: "Teachers");

            migrationBuilder.RenameTable(
                name: "StudentEntity",
                newName: "Students");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTeacherMap_Students_StudentId",
                table: "StudentTeacherMap",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTeacherMap_Teachers_TeacherId",
                table: "StudentTeacherMap",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentTeacherMap_Students_StudentId",
                table: "StudentTeacherMap");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentTeacherMap_Teachers_TeacherId",
                table: "StudentTeacherMap");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "Teachers",
                newName: "TeacherEntity");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "StudentEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherEntity",
                table: "TeacherEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentEntity",
                table: "StudentEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTeacherMap_StudentEntity_StudentId",
                table: "StudentTeacherMap",
                column: "StudentId",
                principalTable: "StudentEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTeacherMap_TeacherEntity_TeacherId",
                table: "StudentTeacherMap",
                column: "TeacherId",
                principalTable: "TeacherEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
