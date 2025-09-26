using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Infraestructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Cambioendominio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Pays");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Fees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "InitialDate",
                table: "Fees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsCancelled",
                table: "Fees",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Fees");

            migrationBuilder.DropColumn(
                name: "InitialDate",
                table: "Fees");

            migrationBuilder.DropColumn(
                name: "IsCancelled",
                table: "Fees");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Pays",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
