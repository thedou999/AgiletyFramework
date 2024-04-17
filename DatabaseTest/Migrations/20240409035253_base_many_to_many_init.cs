using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseTest.Migrations
{
    /// <inheritdoc />
    public partial class base_many_to_many_init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeacherEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentTeacherMap",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTeacherMap", x => new { x.StudentId, x.TeacherId });
                    table.ForeignKey(
                        name: "FK_StudentTeacherMap_StudentEntity_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentTeacherMap_TeacherEntity_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "TeacherEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentTeacherMap_TeacherId",
                table: "StudentTeacherMap",
                column: "TeacherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentTeacherMap");

            migrationBuilder.DropTable(
                name: "StudentEntity");

            migrationBuilder.DropTable(
                name: "TeacherEntity");
        }
    }
}
