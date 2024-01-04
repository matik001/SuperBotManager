using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperBotManagerBackend.Migrations
{
    /// <inheritdoc />
    public partial class ChangedActionTemplateToActionExecutor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "actiondefinition",
                keyColumn: "Id",
                keyValue: -26666654);

            migrationBuilder.InsertData(
                table: "actiondefinition",
                columns: new[] { "Id", "ActionDataSchema", "ActionDefinitionName", "CreatedDate", "ModifiedDate" },
                values: new object[] { 600801797, "{\"InputSchema\":[{\"Name\":\"Email\",\"Description\":\"Address email for your new account\",\"Type\":0},{\"Name\":\"Password\",\"Description\":\"Password for your new account\",\"Type\":0},{\"Name\":\"CardNumber\",\"Description\":\"Card number eg. 1234 1234 1234 1234\",\"Type\":0},{\"Name\":\"CardCCV\",\"Description\":\"Card CCV numer eg. 321\",\"Type\":1},{\"Name\":\"CardExpiration\",\"Description\":\"Card Expiration with format MM/YY eg. 07/25\",\"Type\":0}],\"OutputSchema\":[{\"Name\":\"Successful\",\"Description\":\"\",\"Type\":0},{\"Name\":\"Message\",\"Description\":\"\",\"Type\":0}]}", "SignUpStorytel", new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "actiondefinition",
                keyColumn: "Id",
                keyValue: 600801797);

            migrationBuilder.InsertData(
                table: "actiondefinition",
                columns: new[] { "Id", "ActionDataSchema", "ActionDefinitionName", "CreatedDate", "ModifiedDate" },
                values: new object[] { -26666654, "{\"InputSchema\":[{\"Name\":\"Email\",\"Description\":\"Address email for your new account\",\"Type\":0},{\"Name\":\"Password\",\"Description\":\"Password for your new account\",\"Type\":0},{\"Name\":\"CardNumber\",\"Description\":\"Card number eg. 1234 1234 1234 1234\",\"Type\":0},{\"Name\":\"CardCCV\",\"Description\":\"Card CCV numer eg. 321\",\"Type\":1},{\"Name\":\"CardExpiration\",\"Description\":\"Card Expiration with format MM/YY eg. 07/25\",\"Type\":0}],\"OutputSchema\":[{\"Name\":\"Successful\",\"Description\":\"\",\"Type\":0},{\"Name\":\"Message\",\"Description\":\"\",\"Type\":0}]}", "SignUpStorytel", new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc) });
        }
    }
}
