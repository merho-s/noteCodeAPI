using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace noteCodeAPI.Migrations
{
    /// <inheritdoc />
    public partial class m7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "code",
                table: "note");

            migrationBuilder.DropColumn(
                name: "image",
                table: "note");

            migrationBuilder.CreateTable(
                name: "code_snippet",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    noteid = table.Column<int>(name: "note_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_code_snippet", x => x.id);
                    table.ForeignKey(
                        name: "FK_code_snippet_note_note_id",
                        column: x => x.noteid,
                        principalTable: "note",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_code_snippet_note_id",
                table: "code_snippet",
                column: "note_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "code_snippet");

            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "note",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "note",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
