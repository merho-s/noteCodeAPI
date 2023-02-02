using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace noteCodeAPI.Migrations
{
    /// <inheritdoc />
    public partial class m3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CodetagNote");

            migrationBuilder.CreateTable(
                name: "notes_tags",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    noteid = table.Column<int>(name: "note_id", type: "int", nullable: false),
                    tagid = table.Column<int>(name: "tag_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notes_tags", x => x.id);
                    table.ForeignKey(
                        name: "FK_notes_tags_codetag_tag_id",
                        column: x => x.tagid,
                        principalTable: "codetag",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_notes_tags_note_note_id",
                        column: x => x.noteid,
                        principalTable: "note",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_notes_tags_note_id",
                table: "notes_tags",
                column: "note_id");

            migrationBuilder.CreateIndex(
                name: "IX_notes_tags_tag_id",
                table: "notes_tags",
                column: "tag_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notes_tags");

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
    }
}
