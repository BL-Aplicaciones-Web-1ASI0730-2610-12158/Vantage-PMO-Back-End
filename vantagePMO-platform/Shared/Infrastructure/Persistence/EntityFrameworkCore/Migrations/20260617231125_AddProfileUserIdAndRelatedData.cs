using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class AddProfileUserIdAndRelatedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "profiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "endorsements",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    quote = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    author_name = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    author_role = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    author_avatar_seed = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_endorsements", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "profile_skills",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    percentage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_profile_skills", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "profile_stats",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    total_projects = table.Column<int>(type: "int", nullable: false),
                    on_track = table.Column<int>(type: "int", nullable: false),
                    at_risk = table.Column<int>(type: "int", nullable: false),
                    trend = table.Column<int>(type: "int", nullable: false),
                    portfolio_health = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    attention_items = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_profile_stats", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "i_x_profiles_user_id",
                table: "profiles",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_endorsements_user_id",
                table: "endorsements",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "i_x_profile_skills_user_id",
                table: "profile_skills",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "i_x_profile_stats_user_id",
                table: "profile_stats",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "endorsements");

            migrationBuilder.DropTable(
                name: "profile_skills");

            migrationBuilder.DropTable(
                name: "profile_stats");

            migrationBuilder.DropIndex(
                name: "i_x_profiles_user_id",
                table: "profiles");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "profiles");
        }
    }
}
