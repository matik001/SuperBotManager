using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SuperBotManagerBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddedCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_action_actionexecutor_ActionExecutorId",
                table: "action");

            migrationBuilder.AddForeignKey(
                name: "FK_action_actionexecutor_ActionExecutorId",
                table: "action",
                column: "ActionExecutorId",
                principalTable: "actionexecutor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_action_actionexecutor_ActionExecutorId",
                table: "action");


            migrationBuilder.AddForeignKey(
                name: "FK_action_actionexecutor_ActionExecutorId",
                table: "action",
                column: "ActionExecutorId",
                principalTable: "actionexecutor",
                principalColumn: "Id");
        }
    }
}
