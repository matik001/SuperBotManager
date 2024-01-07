using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SuperBotManagerBackend.Migrations
{
    /// <inheritdoc />
    public partial class ChangedFieldsNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "actiondefinition",
                keyColumn: "Id",
                keyValue: -445203029);

            migrationBuilder.DeleteData(
                table: "actiondefinition",
                keyColumn: "Id",
                keyValue: -252066601);

            migrationBuilder.InsertData(
                table: "actiondefinition",
                columns: new[] { "Id", "ActionDataSchema", "ActionDefinitionDescription", "ActionDefinitionIcon", "ActionDefinitionName", "CreatedDate", "ModifiedDate", "PreserveExecutedInputs" },
                values: new object[,]
                {
                    { 400536037, "{\"InputSchema\":[{\"Name\":\"Email\",\"Description\":\"Address email for your new account\",\"Type\":0,\"IsOptional\":false},{\"Name\":\"Password\",\"Description\":\"Password for your new account\",\"Type\":2,\"IsOptional\":false},{\"Name\":\"Card number\",\"Description\":\"Card number eg. 1234 1234 1234 1234\",\"Type\":0,\"IsOptional\":false},{\"Name\":\"Card CCV\",\"Description\":\"Card CCV numer eg. 321\",\"Type\":2,\"IsOptional\":false},{\"Name\":\"Card expiration\",\"Description\":\"Card expiration date (only year and month matter)\",\"Type\":4,\"IsOptional\":false}],\"OutputSchema\":[{\"Name\":\"Successful\",\"Description\":\"True if account was created\",\"Type\":5,\"IsOptional\":false},{\"Name\":\"Message\",\"Description\":\"Result message\",\"Type\":0,\"IsOptional\":false}]}", "Create an account in storytel", "/storytel.png", "Storytel - sign up", new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), false },
                    { 1279897748, "{\"InputSchema\":[{\"Name\":\"Trip date\",\"Description\":\"What day do You want ticket for?\",\"Type\":3,\"IsOptional\":false},{\"Name\":\"Ticket owner\",\"Description\":\"Owner of the ticket (real Firstname and Lastname)\",\"Type\":0,\"IsOptional\":false},{\"Name\":\"From\",\"Description\":\"First station where you begin trip\",\"Type\":0,\"IsOptional\":false},{\"Name\":\"To\",\"Description\":\"Last station - end of trip\",\"Type\":0,\"IsOptional\":false},{\"Name\":\"Login\",\"Description\":\"Login for intercity\",\"Type\":0,\"IsOptional\":false},{\"Name\":\"Password\",\"Description\":\"Password for intercity\",\"Type\":2,\"IsOptional\":false},{\"Name\":\"Discount\",\"Description\":\"Pick your discount\",\"Type\":7,\"IsOptional\":false,\"SetOptions\":[{\"Display\":\"None\",\"Value\":\"None\"},{\"Display\":\"Student\",\"Value\":\"Student\"}]}],\"OutputSchema\":[{\"Name\":\"Successful\",\"Description\":\"True if ticket was ordered. You have 10 minutes to pay for it.\",\"Type\":5,\"IsOptional\":false},{\"Name\":\"Message\",\"Description\":\"Result message\",\"Type\":0,\"IsOptional\":false}]}", "Buy ticket for intercity", "/intercity.jpg", "Intercity - buy ticket", new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "actiondefinition",
                keyColumn: "Id",
                keyValue: 400536037);

            migrationBuilder.DeleteData(
                table: "actiondefinition",
                keyColumn: "Id",
                keyValue: 1279897748);

            migrationBuilder.InsertData(
                table: "actiondefinition",
                columns: new[] { "Id", "ActionDataSchema", "ActionDefinitionDescription", "ActionDefinitionIcon", "ActionDefinitionName", "CreatedDate", "ModifiedDate", "PreserveExecutedInputs" },
                values: new object[,]
                {
                    { -445203029, "{\"InputSchema\":[{\"Name\":\"Trip date\",\"Description\":\"What day do You want ticket for?\",\"Type\":3,\"IsOptional\":false},{\"Name\":\"TicketOwner\",\"Description\":\"Owner of the ticket (real Firstname and Lastname)\",\"Type\":0,\"IsOptional\":false},{\"Name\":\"From\",\"Description\":\"First station where you begin trip\",\"Type\":0,\"IsOptional\":false},{\"Name\":\"To\",\"Description\":\"Last station - end of trip\",\"Type\":0,\"IsOptional\":false},{\"Name\":\"Login\",\"Description\":\"Login for intercity\",\"Type\":0,\"IsOptional\":false},{\"Name\":\"Password\",\"Description\":\"Password for intercity\",\"Type\":2,\"IsOptional\":false},{\"Name\":\"Discount\",\"Description\":\"Pick your discount\",\"Type\":7,\"IsOptional\":false,\"SetOptions\":[{\"Display\":\"None\",\"Value\":\"None\"},{\"Display\":\"Student\",\"Value\":\"Student\"}]}],\"OutputSchema\":[{\"Name\":\"Successful\",\"Description\":\"True if ticket was ordered. You have 10 minutes to pay for it.\",\"Type\":5,\"IsOptional\":false},{\"Name\":\"Message\",\"Description\":\"Result message\",\"Type\":0,\"IsOptional\":false}]}", "Buy ticket for intercity", "/intercity.jpg", "Intercity - buy ticket", new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { -252066601, "{\"InputSchema\":[{\"Name\":\"Email\",\"Description\":\"Address email for your new account\",\"Type\":0,\"IsOptional\":false},{\"Name\":\"Password\",\"Description\":\"Password for your new account\",\"Type\":2,\"IsOptional\":false},{\"Name\":\"CardNumber\",\"Description\":\"Card number eg. 1234 1234 1234 1234\",\"Type\":0,\"IsOptional\":false},{\"Name\":\"CardCCV\",\"Description\":\"Card CCV numer eg. 321\",\"Type\":2,\"IsOptional\":false},{\"Name\":\"CardExpiration\",\"Description\":\"Card expiration date (only year and month matter)\",\"Type\":4,\"IsOptional\":false}],\"OutputSchema\":[{\"Name\":\"Successful\",\"Description\":\"True if account was created\",\"Type\":5,\"IsOptional\":false},{\"Name\":\"Message\",\"Description\":\"Result message\",\"Type\":0,\"IsOptional\":false}]}", "Create an account in storytel", "/storytel.png", "Storytel - sign up", new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), false }
                });
        }
    }
}
