using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace noteCodeAPI.Migrations
{
    /// <inheritdoc />
    public partial class renametablesnamestoplural : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_code_snippet_note_note_id",
                table: "code_snippet");

            migrationBuilder.DropForeignKey(
                name: "FK_codetag_alias_codetag_codetag_id",
                table: "codetag_alias");

            migrationBuilder.DropForeignKey(
                name: "FK_note_users_user_id",
                table: "note");

            migrationBuilder.DropForeignKey(
                name: "FK_notes_tags_codetag_tag_id",
                table: "notes_tags");

            migrationBuilder.DropForeignKey(
                name: "FK_notes_tags_note_note_id",
                table: "notes_tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_unused_active_token",
                table: "unused_active_token");

            migrationBuilder.DropPrimaryKey(
                name: "PK_note",
                table: "note");

            migrationBuilder.DropPrimaryKey(
                name: "PK_codetag",
                table: "codetag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_code_snippet",
                table: "code_snippet");

            migrationBuilder.RenameTable(
                name: "unused_active_token",
                newName: "unused_active_tokens");

            migrationBuilder.RenameTable(
                name: "note",
                newName: "notes");

            migrationBuilder.RenameTable(
                name: "codetag",
                newName: "codetags");

            migrationBuilder.RenameTable(
                name: "code_snippet",
                newName: "code_snippets");

            migrationBuilder.RenameIndex(
                name: "IX_note_user_id",
                table: "notes",
                newName: "IX_notes_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_code_snippet_note_id",
                table: "code_snippets",
                newName: "IX_code_snippets_note_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_unused_active_tokens",
                table: "unused_active_tokens",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_notes",
                table: "notes",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_codetags",
                table: "codetags",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_code_snippets",
                table: "code_snippets",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_code_snippets_notes_note_id",
                table: "code_snippets",
                column: "note_id",
                principalTable: "notes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_codetag_alias_codetags_codetag_id",
                table: "codetag_alias",
                column: "codetag_id",
                principalTable: "codetags",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_notes_users_user_id",
                table: "notes",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_notes_tags_codetags_tag_id",
                table: "notes_tags",
                column: "tag_id",
                principalTable: "codetags",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_notes_tags_notes_note_id",
                table: "notes_tags",
                column: "note_id",
                principalTable: "notes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_code_snippets_notes_note_id",
                table: "code_snippets");

            migrationBuilder.DropForeignKey(
                name: "FK_codetag_alias_codetags_codetag_id",
                table: "codetag_alias");

            migrationBuilder.DropForeignKey(
                name: "FK_notes_users_user_id",
                table: "notes");

            migrationBuilder.DropForeignKey(
                name: "FK_notes_tags_codetags_tag_id",
                table: "notes_tags");

            migrationBuilder.DropForeignKey(
                name: "FK_notes_tags_notes_note_id",
                table: "notes_tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_unused_active_tokens",
                table: "unused_active_tokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_notes",
                table: "notes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_codetags",
                table: "codetags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_code_snippets",
                table: "code_snippets");

            migrationBuilder.RenameTable(
                name: "unused_active_tokens",
                newName: "unused_active_token");

            migrationBuilder.RenameTable(
                name: "notes",
                newName: "note");

            migrationBuilder.RenameTable(
                name: "codetags",
                newName: "codetag");

            migrationBuilder.RenameTable(
                name: "code_snippets",
                newName: "code_snippet");

            migrationBuilder.RenameIndex(
                name: "IX_notes_user_id",
                table: "note",
                newName: "IX_note_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_code_snippets_note_id",
                table: "code_snippet",
                newName: "IX_code_snippet_note_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_unused_active_token",
                table: "unused_active_token",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_note",
                table: "note",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_codetag",
                table: "codetag",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_code_snippet",
                table: "code_snippet",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_code_snippet_note_note_id",
                table: "code_snippet",
                column: "note_id",
                principalTable: "note",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_codetag_alias_codetag_codetag_id",
                table: "codetag_alias",
                column: "codetag_id",
                principalTable: "codetag",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_note_users_user_id",
                table: "note",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id");

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
    }
}
