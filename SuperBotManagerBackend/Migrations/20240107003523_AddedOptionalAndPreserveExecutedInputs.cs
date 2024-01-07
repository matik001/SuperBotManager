using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SuperBotManagerBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddedOptionalAndPreserveExecutedInputs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "actiondefinition",
                keyColumn: "Id",
                keyValue: -1328912865);

            migrationBuilder.DeleteData(
                table: "actiondefinition",
                keyColumn: "Id",
                keyValue: -757997869);

            migrationBuilder.AddColumn<bool>(
                name: "PreserveExecutedInputs",
                table: "actionexecutor",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PreserveExecutedInputs",
                table: "actiondefinition",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "actiondefinition",
                columns: new[] { "Id", "ActionDataSchema", "ActionDefinitionDescription", "ActionDefinitionIcon", "ActionDefinitionName", "CreatedDate", "ModifiedDate", "PreserveExecutedInputs" },
                values: new object[,]
                {
                    { -1143126163, "{\"InputSchema\":[{\"Name\":\"Trip date\",\"Description\":\"What day do You want ticket for?\",\"Type\":2,\"IsOptional\":false},{\"Name\":\"TicketOwner\",\"Description\":\"Owner of the ticket (real Firstname and Lastname)\",\"Type\":0,\"IsOptional\":false},{\"Name\":\"From\",\"Description\":\"First station where you begin trip\",\"Type\":0,\"IsOptional\":false},{\"Name\":\"To\",\"Description\":\"Last station - end of trip\",\"Type\":0,\"IsOptional\":false},{\"Name\":\"Login\",\"Description\":\"Login for intercity\",\"Type\":0,\"IsOptional\":false},{\"Name\":\"Password\",\"Description\":\"Password for intercity\",\"Type\":0,\"IsOptional\":false},{\"Name\":\"Discount\",\"Description\":\"Pick your discount\",\"Type\":6,\"IsOptional\":false,\"SetOptions\":[{\"Display\":\"None\",\"Value\":\"None\"},{\"Display\":\"Student\",\"Value\":\"Student\"}]}],\"OutputSchema\":[{\"Name\":\"Successful\",\"Description\":\"True if ticket was ordered. You have 10 minutes to pay for it.\",\"Type\":4,\"IsOptional\":false},{\"Name\":\"Message\",\"Description\":\"Result message\",\"Type\":0,\"IsOptional\":false}]}", "Buy ticket for intercity", "/intercity.jpg", "IntercityBuyTicket", new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), false },
                    { 2030954939, "{\"InputSchema\":[{\"Name\":\"Email\",\"Description\":\"Address email for your new account\",\"Type\":0,\"IsOptional\":false},{\"Name\":\"Password\",\"Description\":\"Password for your new account\",\"Type\":0,\"IsOptional\":false},{\"Name\":\"CardNumber\",\"Description\":\"Card number eg. 1234 1234 1234 1234\",\"Type\":0,\"IsOptional\":false},{\"Name\":\"CardCCV\",\"Description\":\"Card CCV numer eg. 321\",\"Type\":1,\"IsOptional\":false},{\"Name\":\"CardExpiration\",\"Description\":\"Card Expiration with format MM/YY eg. 07/25\",\"Type\":0,\"IsOptional\":false}],\"OutputSchema\":[{\"Name\":\"Successful\",\"Description\":\"True if account was created\",\"Type\":4,\"IsOptional\":false},{\"Name\":\"Message\",\"Description\":\"Result message\",\"Type\":0,\"IsOptional\":false}]}", "Create an account in storytel", "/storytel.png", "SignUpStorytel", new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "actiondefinition",
                keyColumn: "Id",
                keyValue: -1143126163);

            migrationBuilder.DeleteData(
                table: "actiondefinition",
                keyColumn: "Id",
                keyValue: 2030954939);

            migrationBuilder.DropColumn(
                name: "PreserveExecutedInputs",
                table: "actionexecutor");

            migrationBuilder.DropColumn(
                name: "PreserveExecutedInputs",
                table: "actiondefinition");

            migrationBuilder.InsertData(
                table: "actiondefinition",
                columns: new[] { "Id", "ActionDataSchema", "ActionDefinitionDescription", "ActionDefinitionIcon", "ActionDefinitionName", "CreatedDate", "ModifiedDate" },
                values: new object[,]
                {
                    { -1328912865, "{\"InputSchema\":[{\"Name\":\"Trip date\",\"Description\":\"What day do You want ticket for?\",\"Type\":2},{\"Name\":\"TicketOwner\",\"Description\":\"Owner of the ticket (real Firstname and Lastname)\",\"Type\":0},{\"Name\":\"From\",\"Description\":\"First station where you begin trip\",\"Type\":0},{\"Name\":\"To\",\"Description\":\"Last station - end of trip\",\"Type\":0},{\"Name\":\"Login\",\"Description\":\"Login for intercity\",\"Type\":0},{\"Name\":\"Password\",\"Description\":\"Password for intercity\",\"Type\":0},{\"Name\":\"Discount\",\"Description\":\"Pick your discount\",\"Type\":6,\"SetOptions\":[{\"Display\":\"None\",\"Value\":\"None\"},{\"Display\":\"Student\",\"Value\":\"Student\"}]}],\"OutputSchema\":[{\"Name\":\"Successful\",\"Description\":\"True if ticket was ordered. You have 10 minutes to pay for it.\",\"Type\":4},{\"Name\":\"Message\",\"Description\":\"Result message\",\"Type\":0}]}", "Buy ticket for intercity", "/intercity.jpg", "IntercityBuyTicket", new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { -757997869, "{\"InputSchema\":[{\"Name\":\"Email\",\"Description\":\"Address email for your new account\",\"Type\":0},{\"Name\":\"Password\",\"Description\":\"Password for your new account\",\"Type\":0},{\"Name\":\"CardNumber\",\"Description\":\"Card number eg. 1234 1234 1234 1234\",\"Type\":0},{\"Name\":\"CardCCV\",\"Description\":\"Card CCV numer eg. 321\",\"Type\":1},{\"Name\":\"CardExpiration\",\"Description\":\"Card Expiration with format MM/YY eg. 07/25\",\"Type\":0}],\"OutputSchema\":[{\"Name\":\"Successful\",\"Description\":\"True if account was created\",\"Type\":4},{\"Name\":\"Message\",\"Description\":\"Result message\",\"Type\":0}]}", "Create an account in storytel", "/storytel.png", "SignUpStorytel", new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc) }
                });
        }
    }
}
