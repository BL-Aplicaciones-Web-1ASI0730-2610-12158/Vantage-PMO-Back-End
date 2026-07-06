using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class AddUserScheduleEntriesSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_schedule_entries",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    time = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    duration = table.Column<int>(type: "int", nullable: false, defaultValue: 60),
                    title = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    detail = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_user_schedule_entries", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "i_x_user_schedule_entries_active",
                table: "user_schedule_entries",
                column: "active");

            migrationBuilder.CreateIndex(
                name: "i_x_user_schedule_entries_user_id",
                table: "user_schedule_entries",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "i_x_user_schedule_entries_user_id_date",
                table: "user_schedule_entries",
                columns: new[] { "user_id", "date" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_schedule_entries");
        }
    }
}
