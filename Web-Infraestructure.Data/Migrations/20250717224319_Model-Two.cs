using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Infraestructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModelTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogClientsRegister",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogClientsRegister", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogClientsRegister_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LogClientsRegister_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogEmployeesRegister",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewEmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RegisterById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEmployeesRegister", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogEmployeesRegister_Employees_NewEmployeeId",
                        column: x => x.NewEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LogEmployeesRegister_Employees_RegisterById",
                        column: x => x.RegisterById,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogClientsRegister_ClientId",
                table: "LogClientsRegister",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_LogClientsRegister_EmployeeId",
                table: "LogClientsRegister",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LogEmployeesRegister_NewEmployeeId",
                table: "LogEmployeesRegister",
                column: "NewEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LogEmployeesRegister_RegisterById",
                table: "LogEmployeesRegister",
                column: "RegisterById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogClientsRegister");

            migrationBuilder.DropTable(
                name: "LogEmployeesRegister");
        }
    }
}
