using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace noteCodeAPI.Migrations
{
    /// <inheritdoc />
    public partial class renamingjointtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CodetagNote_codetag_CodetagsId",
                table: "CodetagNote");

            migrationBuilder.DropForeignKey(
                name: "FK_CodetagNote_note_NotesId",
                table: "CodetagNote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CodetagNote",
                table: "CodetagNote");

            migrationBuilder.RenameTable(
                name: "CodetagNote",
                newName: "notes_tags");

            migrationBuilder.RenameColumn(
                name: "NotesId",
                table: "notes_tags",
                newName: "note_id");

            migrationBuilder.RenameColumn(
                name: "CodetagsId",
                table: "notes_tags",
                newName: "tag_id");

            migrationBuilder.RenameIndex(
                name: "IX_CodetagNote_NotesId",
                table: "notes_tags",
                newName: "IX_notes_tags_note_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_notes_tags",
                table: "notes_tags",
                columns: new[] { "tag_id", "note_id" });

            migrationBuilder.AddForeignKey(
                name: "FK_notes_tags_codetag_tag_id",
                table: "notes_tags",
                column: "tag_id",
                principalTable: "codetag",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_notes_tags_note_note_id",
                table: "notes_tags",
                column: "note_id",
                principalTable: "note",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notes_tags_codetag_tag_id",
                table: "notes_tags");

            migrationBuilder.DropForeignKey(
                name: "FK_notes_tags_note_note_id",
                table: "notes_tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_notes_tags",
                table: "notes_tags");

            migrationBuilder.RenameTable(
                name: "notes_tags",
                newName: "CodetagNote");

            migrationBuilder.RenameColumn(
                name: "note_id",
                table: "CodetagNote",
                newName: "NotesId");

            migrationBuilder.RenameColumn(
                name: "tag_id",
                table: "CodetagNote",
                newName: "CodetagsId");

            migrationBuilder.RenameIndex(
                name: "IX_notes_tags_note_id",
                table: "CodetagNote",
                newName: "IX_CodetagNote_NotesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CodetagNote",
                table: "CodetagNote",
                columns: new[] { "CodetagsId", "NotesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CodetagNote_codetag_CodetagsId",
                table: "CodetagNote",
                column: "CodetagsId",
                principalTable: "codetag",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CodetagNote_note_NotesId",
                table: "CodetagNote",
                column: "NotesId",
                principalTable: "note",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
