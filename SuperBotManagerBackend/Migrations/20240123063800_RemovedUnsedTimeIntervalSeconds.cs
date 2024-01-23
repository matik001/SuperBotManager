using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperBotManagerBackend.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUnsedTimeIntervalSeconds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeIntervalSeconds",
                table: "actionexecutor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimeIntervalSeconds",
                table: "actionexecutor",
                type: "int",
                nullable: true);
        }
    }
}
