using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage2._0.Migrations
{
    /// <inheritdoc />
    public partial class ParkeringsIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParkeringsIndex",
                table: "ParkeratFordon",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ParkeratFordon",
                keyColumn: "Id",
                keyValue: 1,
                column: "ParkeringsIndex",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ParkeratFordon",
                keyColumn: "Id",
                keyValue: 2,
                column: "ParkeringsIndex",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ParkeratFordon",
                keyColumn: "Id",
                keyValue: 3,
                column: "ParkeringsIndex",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ParkeratFordon",
                keyColumn: "Id",
                keyValue: 4,
                column: "ParkeringsIndex",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ParkeratFordon",
                keyColumn: "Id",
                keyValue: 5,
                column: "ParkeringsIndex",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParkeringsIndex",
                table: "ParkeratFordon");
        }
    }
}
