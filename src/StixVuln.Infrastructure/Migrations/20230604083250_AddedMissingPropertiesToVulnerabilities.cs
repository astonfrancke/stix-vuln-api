using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StixVuln.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedMissingPropertiesToVulnerabilities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Vulnerabilities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ExternalReferences",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "ExternalReferences",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceName",
                table: "ExternalReferences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "ExternalReferences",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Vulnerabilities");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ExternalReferences");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "ExternalReferences");

            migrationBuilder.DropColumn(
                name: "SourceName",
                table: "ExternalReferences");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "ExternalReferences");
        }
    }
}
