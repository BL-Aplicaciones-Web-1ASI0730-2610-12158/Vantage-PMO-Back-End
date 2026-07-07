using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class AddRiskComplianceSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "compliance_metrics",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    integrity_pulse = table.Column<double>(type: "double", nullable: false),
                    integrity_delta = table.Column<double>(type: "double", nullable: false),
                    doc_compliance = table.Column<int>(type: "int", nullable: false),
                    sla_compliance = table.Column<int>(type: "int", nullable: false),
                    system_integrity_alerts = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_compliance_metrics", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "risk_matrices",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    segment = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    environment = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    heatmap_cells = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_risk_matrices", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "risks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    risk_id = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    description = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false),
                    severity = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    likelihood = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    impact = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    status = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    audit_trail = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    audit_date = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    flagged_by = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    days_open = table.Column<int>(type: "int", nullable: false),
                    has_action_plan = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    segment = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    control_log = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_risks", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "compliance_metrics");

            migrationBuilder.DropTable(
                name: "risk_matrices");

            migrationBuilder.DropTable(
                name: "risks");
        }
    }
}
