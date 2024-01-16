using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperBotManagerBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddedFieldsToAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActionOnFinishId",
                table: "action",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RunStartType",
                table: "action",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_action_ActionOnFinishId",
                table: "action",
                column: "ActionOnFinishId");

            migrationBuilder.AddForeignKey(
                name: "FK_action_action_ActionOnFinishId",
                table: "action",
                column: "ActionOnFinishId",
                principalTable: "action",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_action_action_ActionOnFinishId",
                table: "action");

            migrationBuilder.DropIndex(
                name: "IX_action_ActionOnFinishId",
                table: "action");

            migrationBuilder.DropColumn(
                name: "ActionOnFinishId",
                table: "action");

            migrationBuilder.DropColumn(
                name: "RunStartType",
                table: "action");
        }
    }
}
