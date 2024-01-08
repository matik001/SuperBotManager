using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SuperBotManagerBase.Migrations
{
    /// <inheritdoc />
    public partial class ResetedMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "actiondefinition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActionDefinitionName = table.Column<string>(type: "text", nullable: false),
                    ActionDefinitionDescription = table.Column<string>(type: "text", nullable: false),
                    ActionDefinitionIcon = table.Column<string>(type: "text", nullable: false),
                    ActionDataSchema = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_actiondefinition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleName = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    UserEmail = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "actionexecutor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActionExecutorName = table.Column<string>(type: "text", nullable: false),
                    ActionData = table.Column<string>(type: "text", nullable: false),
                    ActionDefinitionId = table.Column<int>(type: "integer", nullable: false),
                    RunPeriod = table.Column<int>(type: "integer", nullable: false),
                    LastRunDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TimeIntervalSeconds = table.Column<int>(type: "integer", nullable: true),
                    ActionExecutorOnFinishId = table.Column<int>(type: "integer", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_actionexecutor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_actionexecutor_actiondefinition_ActionDefinitionId",
                        column: x => x.ActionDefinitionId,
                        principalTable: "actiondefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_actionexecutor_actionexecutor_ActionExecutorOnFinishId",
                        column: x => x.ActionExecutorOnFinishId,
                        principalTable: "actionexecutor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RolesId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoleUser_role_RolesId",
                        column: x => x.RolesId,
                        principalTable: "role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_user_UsersId",
                        column: x => x.UsersId,
                        principalTable: "user",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "refreshtoken",
                columns: table => new
                {
                    RefreshTokenId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Token = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refreshtoken", x => x.RefreshTokenId);
                    table.ForeignKey(
                        name: "FK_refreshtoken_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "revokedtokens",
                columns: table => new
                {
                    RevokedTokenId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TokenGuid = table.Column<string>(type: "text", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_revokedtokens", x => x.RevokedTokenId);
                    table.ForeignKey(
                        name: "FK_revokedtokens_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userpassword",
                columns: table => new
                {
                    PasswordId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    PasswordDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PasswordUserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userpassword", x => x.PasswordId);
                    table.ForeignKey(
                        name: "FK_userpassword_user_PasswordUserId",
                        column: x => x.PasswordUserId,
                        principalTable: "user",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userrole",
                columns: table => new
                {
                    UserRoleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userrole", x => x.UserRoleId);
                    table.ForeignKey(
                        name: "FK_userrole_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userrole_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "action",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActionData = table.Column<string>(type: "text", nullable: false),
                    ActionStatus = table.Column<int>(type: "integer", nullable: false),
                    ActionExecutorId = table.Column<int>(type: "integer", nullable: true),
                    ErrorId = table.Column<int>(type: "integer", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_action", x => x.Id);
                    table.ForeignKey(
                        name: "FK_action_actionexecutor_ActionExecutorId",
                        column: x => x.ActionExecutorId,
                        principalTable: "actionexecutor",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "actiondefinition",
                columns: new[] { "Id", "ActionDataSchema", "ActionDefinitionDescription", "ActionDefinitionIcon", "ActionDefinitionName", "CreatedDate", "ModifiedDate" },
                values: new object[,]
                {
                    { 999390358, "{\"InputSchema\":[{\"Name\":\"Email\",\"Description\":\"Address email for your new account\",\"Type\":0},{\"Name\":\"Password\",\"Description\":\"Password for your new account\",\"Type\":0},{\"Name\":\"CardNumber\",\"Description\":\"Card number eg. 1234 1234 1234 1234\",\"Type\":0},{\"Name\":\"CardCCV\",\"Description\":\"Card CCV numer eg. 321\",\"Type\":1},{\"Name\":\"CardExpiration\",\"Description\":\"Card Expiration with format MM/YY eg. 07/25\",\"Type\":0}],\"OutputSchema\":[{\"Name\":\"Successful\",\"Description\":\"True if account was created\",\"Type\":4},{\"Name\":\"Message\",\"Description\":\"Result message\",\"Type\":0}]}", "Create an account in storytel", "storytel.png", "SignUpStorytel", new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 1430980592, "{\"InputSchema\":[{\"Name\":\"Trip date\",\"Description\":\"What day do You want ticket for?\",\"Type\":2},{\"Name\":\"TicketOwner\",\"Description\":\"Owner of the ticket (real Firstname and Lastname)\",\"Type\":0},{\"Name\":\"From\",\"Description\":\"First station where you begin trip\",\"Type\":0},{\"Name\":\"To\",\"Description\":\"Last station - end of trip\",\"Type\":0},{\"Name\":\"Login\",\"Description\":\"Login for intercity\",\"Type\":0},{\"Name\":\"Password\",\"Description\":\"Password for intercity\",\"Type\":0},{\"Name\":\"Discount\",\"Description\":\"Pick your discount\",\"Type\":6,\"SetOptions\":[{\"Display\":\"None\",\"Value\":\"None\"},{\"Display\":\"Student\",\"Value\":\"Student\"}]}],\"OutputSchema\":[{\"Name\":\"Successful\",\"Description\":\"True if ticket was ordered. You have 10 minutes to pay for it.\",\"Type\":4},{\"Name\":\"Message\",\"Description\":\"Result message\",\"Type\":0}]}", "Buy ticket for intercity", "intercity.jpg", "IntercityBuyTicket", new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "RoleId", "CreatedDate", "ModifiedDate", "RoleName" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Blocked" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UsersId",
                table: "RoleUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_action_ActionExecutorId",
                table: "action",
                column: "ActionExecutorId");

            migrationBuilder.CreateIndex(
                name: "IX_actionexecutor_ActionDefinitionId",
                table: "actionexecutor",
                column: "ActionDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_actionexecutor_ActionExecutorOnFinishId",
                table: "actionexecutor",
                column: "ActionExecutorOnFinishId");

            migrationBuilder.CreateIndex(
                name: "IX_refreshtoken_UserId",
                table: "refreshtoken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_revokedtokens_UserId",
                table: "revokedtokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_userpassword_PasswordUserId",
                table: "userpassword",
                column: "PasswordUserId");

            migrationBuilder.CreateIndex(
                name: "IX_userrole_RoleId",
                table: "userrole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_userrole_UserId",
                table: "userrole",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropTable(
                name: "action");

            migrationBuilder.DropTable(
                name: "refreshtoken");

            migrationBuilder.DropTable(
                name: "revokedtokens");

            migrationBuilder.DropTable(
                name: "userpassword");

            migrationBuilder.DropTable(
                name: "userrole");

            migrationBuilder.DropTable(
                name: "actionexecutor");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "actiondefinition");
        }
    }
}
