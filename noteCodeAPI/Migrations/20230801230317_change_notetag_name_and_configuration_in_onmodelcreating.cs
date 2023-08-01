using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace noteCodeAPI.Migrations
{
    /// <inheritdoc />
    public partial class changenotetagnameandconfigurationinonmodelcreating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotesTags");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoteTag");

            migrationBuilder.CreateTable(
                name: "NotesTags",
                columns: table => new
                {
                    CodetagsId = table.Column<int>(type: "int", nullable: false),
                    NotesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotesTags", x => new { x.CodetagsId, x.NotesId });
                    table.ForeignKey(
                        name: "FK_NotesTags_codetag_CodetagsId",
                        column: x => x.CodetagsId,
                        principalTable: "codetag",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotesTags_note_NotesId",
                        column: x => x.NotesId,
                        principalTable: "note",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotesTags_NotesId",
                table: "NotesTags",
                column: "NotesId");
        }
    }
}
