using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperBotManagerBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddedCascadeDeleteToActionExecutorOnFinish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_actionexecutor_actionexecutor_ActionExecutorOnFinishId",
                table: "actionexecutor");

            migrationBuilder.AddForeignKey(
                name: "FK_actionexecutor_actionexecutor_ActionExecutorOnFinishId",
                table: "actionexecutor",
                column: "ActionExecutorOnFinishId",
                principalTable: "actionexecutor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_actionexecutor_actionexecutor_ActionExecutorOnFinishId",
                table: "actionexecutor");

            migrationBuilder.AddForeignKey(
                name: "FK_actionexecutor_actionexecutor_ActionExecutorOnFinishId",
                table: "actionexecutor",
                column: "ActionExecutorOnFinishId",
                principalTable: "actionexecutor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
