using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace noteCodeAPI.Migrations
{
    /// <inheritdoc />
    public partial class m0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "codetag",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_codetag", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "note",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userid = table.Column<int>(name: "user_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_note", x => x.id);
                    table.ForeignKey(
                        name: "FK_note_users_user_id",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_note_user_id",
                table: "note",
                column: "user_id");

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

            migrationBuilder.DropTable(
                name: "codetag");

            migrationBuilder.DropTable(
                name: "note");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
