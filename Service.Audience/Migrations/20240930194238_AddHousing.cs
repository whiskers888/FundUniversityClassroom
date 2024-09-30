using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Service.Audience.Migrations
{
    /// <inheritdoc />
    public partial class AddHousing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EFHousingSummary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EFHousingSummary", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_EFAudiences_EFHousingSummary_HousingId",
                table: "EFAudiences",
                column: "HousingId",
                principalTable: "EFHousingSummary",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EFAudiences_EFHousingSummary_HousingId",
                table: "EFAudiences");

            migrationBuilder.DropTable(
                name: "EFHousingSummary");
        }
    }
}
