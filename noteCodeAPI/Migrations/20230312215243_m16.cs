using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace noteCodeAPI.Migrations
{
    /// <inheritdoc />
    public partial class m16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_code_snippet_codetag_alias_tag_alias_id",
                table: "code_snippet");

            migrationBuilder.RenameColumn(
                name: "tag_alias_id",
                table: "code_snippet",
                newName: "TagAliasId");

            migrationBuilder.RenameIndex(
                name: "IX_code_snippet_tag_alias_id",
                table: "code_snippet",
                newName: "IX_code_snippet_TagAliasId");

            migrationBuilder.AddColumn<string>(
                name: "language",
                table: "code_snippet",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_code_snippet_codetag_alias_TagAliasId",
                table: "code_snippet",
                column: "TagAliasId",
                principalTable: "codetag_alias",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_code_snippet_codetag_alias_TagAliasId",
                table: "code_snippet");

            migrationBuilder.DropColumn(
                name: "language",
                table: "code_snippet");

            migrationBuilder.RenameColumn(
                name: "TagAliasId",
                table: "code_snippet",
                newName: "tag_alias_id");

            migrationBuilder.RenameIndex(
                name: "IX_code_snippet_TagAliasId",
                table: "code_snippet",
                newName: "IX_code_snippet_tag_alias_id");

            migrationBuilder.AddForeignKey(
                name: "FK_code_snippet_codetag_alias_tag_alias_id",
                table: "code_snippet",
                column: "tag_alias_id",
                principalTable: "codetag_alias",
                principalColumn: "id");
        }
    }
}
