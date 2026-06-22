using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectsSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    category = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    description = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false),
                    progress = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    start_date = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    end_date = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    due_date = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    manager = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    team_members = table.Column<string>(type: "longtext", nullable: false),
                    milestones = table.Column<string>(type: "longtext", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_projects", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "projects");
        }
    }
}
