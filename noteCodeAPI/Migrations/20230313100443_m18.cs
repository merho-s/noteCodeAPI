using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace noteCodeAPI.Migrations
{
    /// <inheritdoc />
    public partial class m18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_code_snippet_codetag_alias_TagAliasId",
                table: "code_snippet");

            migrationBuilder.DropIndex(
                name: "IX_code_snippet_TagAliasId",
                table: "code_snippet");

            migrationBuilder.DropColumn(
                name: "TagAliasId",
                table: "code_snippet");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TagAliasId",
                table: "code_snippet",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_code_snippet_TagAliasId",
                table: "code_snippet",
                column: "TagAliasId");

            migrationBuilder.AddForeignKey(
                name: "FK_code_snippet_codetag_alias_TagAliasId",
                table: "code_snippet",
                column: "TagAliasId",
                principalTable: "codetag_alias",
                principalColumn: "id");
        }
    }
}
