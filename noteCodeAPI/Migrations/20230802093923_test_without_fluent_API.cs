using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace noteCodeAPI.Migrations
{
    /// <inheritdoc />
    public partial class testwithoutfluentAPI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoteTag");

            migrationBuilder.CreateTable(
                name: "CodetagNote",
                columns: table => new
                {
                    CodetagsId = table.Column<int>(type: "int", nullable: false),
                    NotesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodetagNote", x => new { x.CodetagsId, x.NotesId });
                    table.ForeignKey(
                        name: "FK_CodetagNote_codetag_CodetagsId",
                        column: x => x.CodetagsId,
                        principalTable: "codetag",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CodetagNote_note_NotesId",
                        column: x => x.NotesId,
                        principalTable: "note",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CodetagNote_NotesId",
                table: "CodetagNote",
                column: "NotesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CodetagNote");

            migrationBuilder.CreateTable(
                name: "NoteTag",
                columns: table => new
                {
                    CodetagsId = table.Column<int>(type: "int", nullable: false),
                    NotesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteTag", x => new { x.CodetagsId, x.NotesId });
                    table.ForeignKey(
                        name: "FK_NoteTag_codetag_CodetagsId",
                        column: x => x.CodetagsId,
                        principalTable: "codetag",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NoteTag_note_NotesId",
                        column: x => x.NotesId,
                        principalTable: "note",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NoteTag_NotesId",
                table: "NoteTag",
                column: "NotesId");
        }
    }
}
