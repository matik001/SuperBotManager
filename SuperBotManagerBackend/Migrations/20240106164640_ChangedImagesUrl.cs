using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SuperBotManagerBackend.Migrations
{
    /// <inheritdoc />
    public partial class ChangedImagesUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "actiondefinition",
                keyColumn: "Id",
                keyValue: 999390358);

            migrationBuilder.DeleteData(
                table: "actiondefinition",
                keyColumn: "Id",
                keyValue: 1430980592);

            migrationBuilder.InsertData(
                table: "actiondefinition",
                columns: new[] { "Id", "ActionDataSchema", "ActionDefinitionDescription", "ActionDefinitionIcon", "ActionDefinitionName", "CreatedDate", "ModifiedDate" },
                values: new object[,]
                {
                    { -1328912865, "{\"InputSchema\":[{\"Name\":\"Trip date\",\"Description\":\"What day do You want ticket for?\",\"Type\":2},{\"Name\":\"TicketOwner\",\"Description\":\"Owner of the ticket (real Firstname and Lastname)\",\"Type\":0},{\"Name\":\"From\",\"Description\":\"First station where you begin trip\",\"Type\":0},{\"Name\":\"To\",\"Description\":\"Last station - end of trip\",\"Type\":0},{\"Name\":\"Login\",\"Description\":\"Login for intercity\",\"Type\":0},{\"Name\":\"Password\",\"Description\":\"Password for intercity\",\"Type\":0},{\"Name\":\"Discount\",\"Description\":\"Pick your discount\",\"Type\":6,\"SetOptions\":[{\"Display\":\"None\",\"Value\":\"None\"},{\"Display\":\"Student\",\"Value\":\"Student\"}]}],\"OutputSchema\":[{\"Name\":\"Successful\",\"Description\":\"True if ticket was ordered. You have 10 minutes to pay for it.\",\"Type\":4},{\"Name\":\"Message\",\"Description\":\"Result message\",\"Type\":0}]}", "Buy ticket for intercity", "/intercity.jpg", "IntercityBuyTicket", new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { -757997869, "{\"InputSchema\":[{\"Name\":\"Email\",\"Description\":\"Address email for your new account\",\"Type\":0},{\"Name\":\"Password\",\"Description\":\"Password for your new account\",\"Type\":0},{\"Name\":\"CardNumber\",\"Description\":\"Card number eg. 1234 1234 1234 1234\",\"Type\":0},{\"Name\":\"CardCCV\",\"Description\":\"Card CCV numer eg. 321\",\"Type\":1},{\"Name\":\"CardExpiration\",\"Description\":\"Card Expiration with format MM/YY eg. 07/25\",\"Type\":0}],\"OutputSchema\":[{\"Name\":\"Successful\",\"Description\":\"True if account was created\",\"Type\":4},{\"Name\":\"Message\",\"Description\":\"Result message\",\"Type\":0}]}", "Create an account in storytel", "/storytel.png", "SignUpStorytel", new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "actiondefinition",
                keyColumn: "Id",
                keyValue: -1328912865);

            migrationBuilder.DeleteData(
                table: "actiondefinition",
                keyColumn: "Id",
                keyValue: -757997869);

            migrationBuilder.InsertData(
                table: "actiondefinition",
                columns: new[] { "Id", "ActionDataSchema", "ActionDefinitionDescription", "ActionDefinitionIcon", "ActionDefinitionName", "CreatedDate", "ModifiedDate" },
                values: new object[,]
                {
                    { 999390358, "{\"InputSchema\":[{\"Name\":\"Email\",\"Description\":\"Address email for your new account\",\"Type\":0},{\"Name\":\"Password\",\"Description\":\"Password for your new account\",\"Type\":0},{\"Name\":\"CardNumber\",\"Description\":\"Card number eg. 1234 1234 1234 1234\",\"Type\":0},{\"Name\":\"CardCCV\",\"Description\":\"Card CCV numer eg. 321\",\"Type\":1},{\"Name\":\"CardExpiration\",\"Description\":\"Card Expiration with format MM/YY eg. 07/25\",\"Type\":0}],\"OutputSchema\":[{\"Name\":\"Successful\",\"Description\":\"True if account was created\",\"Type\":4},{\"Name\":\"Message\",\"Description\":\"Result message\",\"Type\":0}]}", "Create an account in storytel", "storytel.png", "SignUpStorytel", new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 1430980592, "{\"InputSchema\":[{\"Name\":\"Trip date\",\"Description\":\"What day do You want ticket for?\",\"Type\":2},{\"Name\":\"TicketOwner\",\"Description\":\"Owner of the ticket (real Firstname and Lastname)\",\"Type\":0},{\"Name\":\"From\",\"Description\":\"First station where you begin trip\",\"Type\":0},{\"Name\":\"To\",\"Description\":\"Last station - end of trip\",\"Type\":0},{\"Name\":\"Login\",\"Description\":\"Login for intercity\",\"Type\":0},{\"Name\":\"Password\",\"Description\":\"Password for intercity\",\"Type\":0},{\"Name\":\"Discount\",\"Description\":\"Pick your discount\",\"Type\":6,\"SetOptions\":[{\"Display\":\"None\",\"Value\":\"None\"},{\"Display\":\"Student\",\"Value\":\"Student\"}]}],\"OutputSchema\":[{\"Name\":\"Successful\",\"Description\":\"True if ticket was ordered. You have 10 minutes to pay for it.\",\"Type\":4},{\"Name\":\"Message\",\"Description\":\"Result message\",\"Type\":0}]}", "Buy ticket for intercity", "intercity.jpg", "IntercityBuyTicket", new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc) }
                });
        }
    }
}
