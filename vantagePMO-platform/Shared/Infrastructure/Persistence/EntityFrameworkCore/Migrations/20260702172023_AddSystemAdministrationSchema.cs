using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class AddSystemAdministrationSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admin_policies",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    password_policy = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    mfa_required = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    session_timeout = table.Column<int>(type: "int", nullable: false),
                    api_request_limits = table.Column<int>(type: "int", nullable: false),
                    notification_permissions = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    jwt_enabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    encrypted_passwords = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    api_protection = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    password_expiration = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    allowed_devices = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ip_restriction = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    minimum_password_length = table.Column<int>(type: "int", nullable: false),
                    require_symbols = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    require_uppercase = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    updated_at = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_admin_policies", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "brandings",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    company_name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    company_description = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false),
                    logo_url = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    primary_color = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    secondary_color = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    typography_style = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    created_at = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    updated_at = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_brandings", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "login_attempts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    timestamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_login_attempts", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "subscriptions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    plan = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    active_users = table.Column<int>(type: "int", nullable: false),
                    billing_cycle = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    expiration_date = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    created_at = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    updated_at = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_subscriptions", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "system_settings",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    email_notifications = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    push_notifications = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    report_alerts = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    admin_alerts = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    project_alerts = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    weekly_digest = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    mention_notifications = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    deadline_reminders = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    browser_push = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    mobile_push = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    sound_alerts = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    security_alerts = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    workspace_updates = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    subscription_alerts = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    team_activity = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    updated_at = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_system_settings", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin_policies");

            migrationBuilder.DropTable(
                name: "brandings");

            migrationBuilder.DropTable(
                name: "login_attempts");

            migrationBuilder.DropTable(
                name: "subscriptions");

            migrationBuilder.DropTable(
                name: "system_settings");
        }
    }
}
