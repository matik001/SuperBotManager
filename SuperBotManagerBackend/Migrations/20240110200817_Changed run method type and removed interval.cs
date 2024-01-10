using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperBotManagerBackend.Migrations
{
    /// <inheritdoc />
    public partial class Changedrunmethodtypeandremovedinterval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RunPeriod",
                table: "actionexecutor",
                newName: "RunMethod");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RunMethod",
                table: "actionexecutor",
                newName: "RunPeriod");
        }
    }
}
