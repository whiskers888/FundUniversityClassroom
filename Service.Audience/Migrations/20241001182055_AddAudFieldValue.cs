using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Service.Audience.Migrations
{
    /// <inheritdoc />
    public partial class AddAudFieldValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EFAudCustomFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EFAudCustomFields", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EFAudCustomFieldsValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AudienceId = table.Column<int>(type: "integer", nullable: false),
                    CustomFieldId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EFAudCustomFieldsValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EFAudCustomFieldsValues_EFAudCustomFields_CustomFieldId",
                        column: x => x.CustomFieldId,
                        principalTable: "EFAudCustomFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EFAudCustomFieldsValues_EFAudiences_AudienceId",
                        column: x => x.AudienceId,
                        principalTable: "EFAudiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EFAudCustomFieldsValues_AudienceId",
                table: "EFAudCustomFieldsValues",
                column: "AudienceId");

            migrationBuilder.CreateIndex(
                name: "IX_EFAudCustomFieldsValues_CustomFieldId",
                table: "EFAudCustomFieldsValues",
                column: "CustomFieldId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EFAudCustomFieldsValues");

            migrationBuilder.DropTable(
                name: "EFAudCustomFields");
        }
    }
}
