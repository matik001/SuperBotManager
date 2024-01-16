using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperBotManagerBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddedForwardedFromAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_action_action_ActionOnFinishId",
                table: "action");

            migrationBuilder.RenameColumn(
                name: "ActionOnFinishId",
                table: "action",
                newName: "ForwardedFromActionId");

            migrationBuilder.RenameIndex(
                name: "IX_action_ActionOnFinishId",
                table: "action",
                newName: "IX_action_ForwardedFromActionId");

            migrationBuilder.AddForeignKey(
                name: "FK_action_action_ForwardedFromActionId",
                table: "action",
                column: "ForwardedFromActionId",
                principalTable: "action",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_action_action_ForwardedFromActionId",
                table: "action");

            migrationBuilder.RenameColumn(
                name: "ForwardedFromActionId",
                table: "action",
                newName: "ActionOnFinishId");

            migrationBuilder.RenameIndex(
                name: "IX_action_ForwardedFromActionId",
                table: "action",
                newName: "IX_action_ActionOnFinishId");

            migrationBuilder.AddForeignKey(
                name: "FK_action_action_ActionOnFinishId",
                table: "action",
                column: "ActionOnFinishId",
                principalTable: "action",
                principalColumn: "Id");
        }
    }
}
