using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class AddReportsSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reports",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    project = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    label = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    manager = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    status = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    completion = table.Column<int>(type: "int", nullable: false),
                    period = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    aggregate_health = table.Column<double>(type: "double", nullable: false),
                    health_trend = table.Column<double>(type: "double", nullable: false),
                    active_risks = table.Column<int>(type: "int", nullable: false),
                    critical_risks = table.Column<int>(type: "int", nullable: false),
                    minor_risks = table.Column<int>(type: "int", nullable: false),
                    budget_variance = table.Column<double>(type: "double", nullable: false),
                    energy_consumption = table.Column<int>(type: "int", nullable: false),
                    automations = table.Column<int>(type: "int", nullable: false),
                    generated_at = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    attachment = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    type = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    velocity_trend = table.Column<string>(type: "longtext", nullable: false),
                    ai_insight = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_reports", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reports");
        }
    }
}
