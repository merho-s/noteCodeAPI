using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace noteCodeAPI.Migrations
{
    /// <inheritdoc />
    public partial class m14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_code_snippet_codetag_alias_language_id",
                table: "code_snippet");

            migrationBuilder.RenameColumn(
                name: "language_id",
                table: "code_snippet",
                newName: "tag_alias_id");

            migrationBuilder.RenameIndex(
                name: "IX_code_snippet_language_id",
                table: "code_snippet",
                newName: "IX_code_snippet_tag_alias_id");

            migrationBuilder.AddForeignKey(
                name: "FK_code_snippet_codetag_alias_tag_alias_id",
                table: "code_snippet",
                column: "tag_alias_id",
                principalTable: "codetag_alias",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_code_snippet_codetag_alias_tag_alias_id",
                table: "code_snippet");

            migrationBuilder.RenameColumn(
                name: "tag_alias_id",
                table: "code_snippet",
                newName: "language_id");

            migrationBuilder.RenameIndex(
                name: "IX_code_snippet_tag_alias_id",
                table: "code_snippet",
                newName: "IX_code_snippet_language_id");

            migrationBuilder.AddForeignKey(
                name: "FK_code_snippet_codetag_alias_language_id",
                table: "code_snippet",
                column: "language_id",
                principalTable: "codetag_alias",
                principalColumn: "id");
        }
    }
}
