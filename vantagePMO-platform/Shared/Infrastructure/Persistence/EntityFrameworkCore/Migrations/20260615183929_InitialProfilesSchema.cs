using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace VantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialProfilesSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "profiles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    email = table.Column<string>(type: "varchar(160)", maxLength: 160, nullable: false),
                    role = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    department = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    joined = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    avatar_seed = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    availability = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    experience = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    delivery_rate = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    active_budget = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    bio = table.Column<string>(type: "longtext", nullable: false),
                    certifications = table.Column<string>(type: "longtext", nullable: false),
                    skills_description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    location = table.Column<string>(type: "varchar(160)", maxLength: 160, nullable: false),
                    years_active = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    availability_label = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_profiles", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "i_x_profiles_email",
                table: "profiles",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "profiles");
        }
    }
}
