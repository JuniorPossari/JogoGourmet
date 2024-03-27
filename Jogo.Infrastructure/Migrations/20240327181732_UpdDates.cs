using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jogo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateUpdated",
                table: "Prato",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdded",
                table: "Prato",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "datetime('now')",
                oldClrType: typeof(DateTimeOffset),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateUpdated",
                table: "Categoria",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdded",
                table: "Categoria",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "datetime('now')",
                oldClrType: typeof(DateTimeOffset),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateUpdated",
                table: "Prato",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateAdded",
                table: "Prato",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "datetime('now')");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateUpdated",
                table: "Categoria",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateAdded",
                table: "Categoria",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "datetime('now')");
        }
    }
}
