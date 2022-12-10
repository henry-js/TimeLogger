using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeLogger.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddClientTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "PreBill",
                table: "Client",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<float>(
                name: "BillingIncrement",
                table: "Client",
                type: "REAL",
                nullable: false,
                defaultValue: 0.25f);

            migrationBuilder.AddColumn<int>(
                name: "CutOff",
                table: "Client",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasCutOff",
                table: "Client",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "MinimumHours",
                table: "Client",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "RoundUpAfterXMinutes",
                table: "Client",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillingIncrement",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "CutOff",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "HasCutOff",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "MinimumHours",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "RoundUpAfterXMinutes",
                table: "Client");

            migrationBuilder.AlterColumn<bool>(
                name: "PreBill",
                table: "Client",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldDefaultValue: false);
        }
    }
}
