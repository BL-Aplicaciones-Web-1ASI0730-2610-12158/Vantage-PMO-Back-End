using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace VantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class AddChatHubSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "chat_insights",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    chat_id = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    meeting_tag = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    time_ago = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    meeting_title = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    insights = table.Column<string>(type: "longtext", nullable: false),
                    sentiment_productive = table.Column<int>(type: "int", nullable: false),
                    sentiment_text = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_chat_insights", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "chat_messages",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    chat_id = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    author_id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    timestamp = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    text = table.Column<string>(type: "varchar(4000)", maxLength: 4000, nullable: false),
                    attachments = table.Column<string>(type: "longtext", nullable: false),
                    reactions = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_chat_messages", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "chat_pinned_assets",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    chat_id = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    name = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    type = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    meta = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_chat_pinned_assets", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "chat_users",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    name = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    avatar_seed = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    avatar = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    status = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    role = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_chat_users", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "chats",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    type = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    members = table.Column<string>(type: "longtext", nullable: false),
                    is_favorited = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_chats", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chat_insights");

            migrationBuilder.DropTable(
                name: "chat_messages");

            migrationBuilder.DropTable(
                name: "chat_pinned_assets");

            migrationBuilder.DropTable(
                name: "chat_users");

            migrationBuilder.DropTable(
                name: "chats");
        }
    }
}
