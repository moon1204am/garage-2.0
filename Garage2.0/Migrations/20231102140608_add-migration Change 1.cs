using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Garage2._0.Migrations
{
    /// <inheritdoc />
    public partial class addmigrationChange1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Modell",
                table: "ParkeratFordon",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Marke",
                table: "ParkeratFordon",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "FordonsTyp",
                table: "ParkeratFordon",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Farg",
                table: "ParkeratFordon",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "AntalHjul",
                table: "ParkeratFordon",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "ParkeratFordon",
                columns: new[] { "Id", "AnkomstTid", "AntalHjul", "Farg", "FordonsTyp", "Marke", "Modell", "RegNr" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 2, 12, 15, 0, 0, DateTimeKind.Unspecified), 4, "Röd", 1, "Toyota", "Prius", "123pop" },
                    { 2, new DateTime(2023, 11, 2, 12, 15, 0, 0, DateTimeKind.Unspecified), 0, "Vit", 2, "Storebror", "Japp", "123båt" },
                    { 3, new DateTime(2023, 11, 2, 12, 45, 0, 0, DateTimeKind.Unspecified), 6, "Blå", 4, "Volvo", "V70", "456pop" },
                    { 4, new DateTime(2023, 11, 2, 12, 25, 0, 0, DateTimeKind.Unspecified), 8, "Vit", 0, "Airbus", "XX90", "783pop" },
                    { 5, new DateTime(2023, 11, 2, 12, 55, 0, 0, DateTimeKind.Unspecified), 2, "Svart", 3, "Mazda", "Vroom", "098pop" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ParkeratFordon",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ParkeratFordon",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ParkeratFordon",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ParkeratFordon",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ParkeratFordon",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AlterColumn<string>(
                name: "Modell",
                table: "ParkeratFordon",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Marke",
                table: "ParkeratFordon",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "FordonsTyp",
                table: "ParkeratFordon",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Farg",
                table: "ParkeratFordon",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "AntalHjul",
                table: "ParkeratFordon",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
