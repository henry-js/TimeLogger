using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeLogger.Domain.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    HourlyRate = table.Column<float>(type: "REAL", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PreBill = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    HasCutOff = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    CutOff = table.Column<int>(type: "INTEGER", nullable: false),
                    MinimumHours = table.Column<float>(type: "REAL", nullable: false, defaultValue: 0f),
                    BillingIncrement = table.Column<float>(type: "REAL", nullable: false, defaultValue: 0.25f),
                    RoundUpAfterXMinutes = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Defaults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HourlyRate = table.Column<float>(type: "REAL", nullable: false),
                    PreBill = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    HasCutOff = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    CutOff = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    MinimumHours = table.Column<float>(type: "REAL", nullable: false, defaultValue: 0f),
                    BillingIncrement = table.Column<float>(type: "REAL", nullable: false, defaultValue: 0.25f),
                    RoundUpAfterXMinutes = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Defaults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Hours = table.Column<float>(type: "REAL", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "(datetime('now', 'localtime'))"),
                    ClientId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Work",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClientId = table.Column<int>(type: "INTEGER", nullable: false),
                    Hours = table.Column<double>(type: "REAL", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    DateEntered = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "(datetime('now', 'localtime'))"),
                    Paid = table.Column<int>(type: "INTEGER", nullable: false),
                    PaymentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Work", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Work_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Work_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ClientId",
                table: "Payments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Work_ClientId",
                table: "Work",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Work_PaymentId",
                table: "Work",
                column: "PaymentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Defaults");

            migrationBuilder.DropTable(
                name: "Work");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Client");
        }
    }
}
