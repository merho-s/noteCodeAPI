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
            migrationBuilder.DropForeignKey(
                name: "FK_note_users_user_id",
                table: "note");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "note",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_note_users_user_id",
                table: "note",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_note_users_user_id",
                table: "note");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "note",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_note_users_user_id",
                table: "note",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
