using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class AddTaskCollaborationSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "p_k_tasks",
                table: "tasks");

            migrationBuilder.RenameTable(
                name: "tasks",
                newName: "dashboard_tasks");

            migrationBuilder.AddPrimaryKey(
                name: "p_k_dashboard_tasks",
                table: "dashboard_tasks",
                column: "id");

            migrationBuilder.CreateTable(
                name: "board_members",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    board_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    role = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    avatar = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    color = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_board_members", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "boards",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    projects_active = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_boards", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "collaboration_tasks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    board_id = table.Column<int>(type: "int", nullable: false),
                    project = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    title = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    description = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false),
                    assignee = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    assignee_avatar = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    assignee_color = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    status = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    priority = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    priority_color = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    completed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    comments = table.Column<int>(type: "int", nullable: false),
                    attachments = table.Column<int>(type: "int", nullable: false),
                    due_date = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    progress = table.Column<int>(type: "int", nullable: false),
                    department = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_collaboration_tasks", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "board_members");

            migrationBuilder.DropTable(
                name: "boards");

            migrationBuilder.DropTable(
                name: "collaboration_tasks");

            migrationBuilder.DropPrimaryKey(
                name: "p_k_dashboard_tasks",
                table: "dashboard_tasks");

            migrationBuilder.RenameTable(
                name: "dashboard_tasks",
                newName: "tasks");

            migrationBuilder.AddPrimaryKey(
                name: "p_k_tasks",
                table: "tasks",
                column: "id");
        }
    }
}
