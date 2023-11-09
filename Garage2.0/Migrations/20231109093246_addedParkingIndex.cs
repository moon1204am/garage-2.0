using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage2._0.Migrations
{
    /// <inheritdoc />
    public partial class addedParkingIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ParkeratFordon",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegNr",
                value: "ABC123");

            migrationBuilder.UpdateData(
                table: "ParkeratFordon",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ParkeringsIndex", "RegNr" },
                values: new object[] { 1, "ABC124" });

            migrationBuilder.UpdateData(
                table: "ParkeratFordon",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ParkeringsIndex", "RegNr" },
                values: new object[] { 4, "ABC125" });

            migrationBuilder.UpdateData(
                table: "ParkeratFordon",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ParkeringsIndex", "RegNr" },
                values: new object[] { 6, "ABC126" });

            migrationBuilder.UpdateData(
                table: "ParkeratFordon",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ParkeringsIndex", "RegNr" },
                values: new object[] { 9, "ABC127" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ParkeratFordon",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegNr",
                value: "123pop");

            migrationBuilder.UpdateData(
                table: "ParkeratFordon",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ParkeringsIndex", "RegNr" },
                values: new object[] { 0, "123båt" });

            migrationBuilder.UpdateData(
                table: "ParkeratFordon",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ParkeringsIndex", "RegNr" },
                values: new object[] { 0, "456pop" });

            migrationBuilder.UpdateData(
                table: "ParkeratFordon",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ParkeringsIndex", "RegNr" },
                values: new object[] { 0, "783pop" });

            migrationBuilder.UpdateData(
                table: "ParkeratFordon",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ParkeringsIndex", "RegNr" },
                values: new object[] { 0, "098pop" });
        }
    }
}
