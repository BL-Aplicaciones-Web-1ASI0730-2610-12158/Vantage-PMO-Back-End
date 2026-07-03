using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class AddUserSettingsSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_settings",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    theme = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    language = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    timezone = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    date_format = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    time_format = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    first_day_of_week = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    currency = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    accent_color = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    density = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    email_notifications = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    push_notifications = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    weekly_digest = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    mention_alerts = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    profile_visibility = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    display_name = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    job_title = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    bio = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false),
                    phone = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    department = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    updated_at = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_user_settings", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_settings");
        }
    }
}
