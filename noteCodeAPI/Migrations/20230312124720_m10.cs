using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace noteCodeAPI.Migrations
{
    /// <inheritdoc />
    public partial class m10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "language",
                table: "code_snippet");

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "code_snippet",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_code_snippet_LanguageId",
                table: "code_snippet",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_code_snippet_codetag_alias_LanguageId",
                table: "code_snippet",
                column: "LanguageId",
                principalTable: "codetag_alias",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_code_snippet_codetag_alias_LanguageId",
                table: "code_snippet");

            migrationBuilder.DropIndex(
                name: "IX_code_snippet_LanguageId",
                table: "code_snippet");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "code_snippet");

            migrationBuilder.AddColumn<string>(
                name: "language",
                table: "code_snippet",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
