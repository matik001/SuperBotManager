﻿using SuperBotManagerBackend.ActionExecutors;
using SuperBotManagerBackend.DB.Repositories;

namespace SuperBotManagerBackend.BotDefinitions
{
    [ActionDefinition]
    public class IntercityBuyTicketActionDefinition
    {
        public static ActionDefinition ActionDefinition { get; } = new ActionDefinition()
        {
            ActionDefinitionName = "IntercityBuyTicket",
            ActionDefinitionDescription = "Buy ticket for intercity",
            ActionDefinitionIcon = "intercity.jpg",
            ActionDataSchema = new ActionDefinitionSchema()
            {
                InputSchema = new List<FieldInfo>()
                {
                    new FieldInfo("Trip date", FieldType.DateTime, "What day do You want ticket for?"),
                    new FieldInfo("TicketOwner", FieldType.String, "Owner of the ticket (real Firstname and Lastname)"),
                    new FieldInfo("From", FieldType.String, "First station where you begin trip"),
                    new FieldInfo("To", FieldType.String, "Last station - end of trip"),
                    new FieldInfo("Login", FieldType.String, "Login for intercity"),
                    new FieldInfo("Password", FieldType.String, "Password for intercity"),
                    new FieldInfo("Discount", FieldType.Set, "Pick your discount")
                    {
                        SetOptions = new List<SetOption>()
                        {
                            new SetOption("None", "None"),
                            new SetOption("Student", "Student"),
                        }
                    },
                },
                OutputSchema = new List<FieldInfo>()
                {
                    new FieldInfo("Successful", FieldType.Boolean, "True if ticket was ordered. You have 10 minutes to pay for it."),
                    new FieldInfo("Message", FieldType.String, "Result message"),
                },
            },
            CreatedDate = DateTime.SpecifyKind(new DateTime(2024, 1, 4), DateTimeKind.Utc),
            ModifiedDate = DateTime.SpecifyKind(new DateTime(2024, 1, 4), DateTimeKind.Utc)
        };
    }
}
