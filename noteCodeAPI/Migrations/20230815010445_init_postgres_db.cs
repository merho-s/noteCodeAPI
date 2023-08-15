using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace noteCodeAPI.Migrations
{
    /// <inheritdoc />
    public partial class initpostgresdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "codetags",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_codetags", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "unused_active_tokens",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    token = table.Column<string>(type: "text", nullable: false),
                    expirationdate = table.Column<DateTime>(name: "expiration_date", type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unused_active_tokens", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "text", nullable: false),
                    passwordhashed = table.Column<string>(name: "password_hashed", type: "text", nullable: false),
                    passwordsalt = table.Column<string>(name: "password_salt", type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    isvalid = table.Column<bool>(name: "is_valid", type: "boolean", nullable: false),
                    role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "notes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    creationdate = table.Column<DateTimeOffset>(name: "creation_date", type: "timestamp with time zone", nullable: false),
                    userid = table.Column<int>(name: "user_id", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notes", x => x.id);
                    table.ForeignKey(
                        name: "FK_notes_users_user_id",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "code_snippets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    language = table.Column<string>(type: "text", nullable: false),
                    noteid = table.Column<Guid>(name: "note_id", type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_code_snippets", x => x.id);
                    table.ForeignKey(
                        name: "FK_code_snippets_notes_note_id",
                        column: x => x.noteid,
                        principalTable: "notes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notes_tags",
                columns: table => new
                {
                    tagid = table.Column<int>(name: "tag_id", type: "integer", nullable: false),
                    noteid = table.Column<Guid>(name: "note_id", type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notes_tags", x => new { x.tagid, x.noteid });
                    table.ForeignKey(
                        name: "FK_notes_tags_codetags_tag_id",
                        column: x => x.tagid,
                        principalTable: "codetags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_notes_tags_notes_note_id",
                        column: x => x.noteid,
                        principalTable: "notes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_code_snippets_note_id",
                table: "code_snippets",
                column: "note_id");

            migrationBuilder.CreateIndex(
                name: "IX_notes_user_id",
                table: "notes",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_notes_tags_note_id",
                table: "notes_tags",
                column: "note_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "code_snippets");

            migrationBuilder.DropTable(
                name: "notes_tags");

            migrationBuilder.DropTable(
                name: "unused_active_tokens");

            migrationBuilder.DropTable(
                name: "codetags");

            migrationBuilder.DropTable(
                name: "notes");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
