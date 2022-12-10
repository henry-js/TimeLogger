using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeLogger.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Default",
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
                    table.PrimaryKey("PK_Default", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Default");
        }
    }
}
