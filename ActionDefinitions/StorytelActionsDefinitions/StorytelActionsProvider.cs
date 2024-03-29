﻿using SuperBotManagerBase.Attributes;
using SuperBotManagerBase.DB.Repositories;

namespace StorytelActionsDefinitions
{
    [ActionsDefinitionProvider("Storytel")]
    public class StorytelActionsProvider
    {
        public static ActionDefinition SignUp { get; } = new ActionDefinition()
        {
            ActionDefinitionQueueName = "storytel-sign-up",
            ActionDefinitionName = "Storytel - sign up",
            ActionDefinitionDescription = "Create an account in storytel",
            ActionDefinitionIcon = "/storytel.png",
            ActionDataSchema = new ActionDefinitionSchema()
            {
                InputSchema = new List<FieldInfo>()
                {
                    new FieldInfo("Email", FieldType.String, "Address email for your new account")
                    {
                        Placeholder = "Enter an email"
                    },
                    new FieldInfo("Password", FieldType.Secret, "Password for your new account")
                    {
                        Placeholder = "Enter a password"
                    },
                    new FieldInfo("Card number", FieldType.String, "Card number eg. 1234 1234 1234 1234")
                    {
                        Placeholder = "Enter a card number"
                    },
                    new FieldInfo("Card CCV", FieldType.Secret, "Card CCV numer eg. 321")
                    {
                        Placeholder = "Enter a card CCV"
                    },
                    new FieldInfo("Card expiration", FieldType.Date, "Card expiration date (only year and month matter)")
                    {
                        Placeholder = "Enter a card expiration date"
                    },
                },
                OutputSchema = new List<FieldInfo>()
                {
                    new FieldInfo("Successful", FieldType.Boolean, "True if account was created"),
                    new FieldInfo("Message", FieldType.String, "Result message"),
                },
            },
            CreatedDate = DateTime.SpecifyKind(new DateTime(2024, 1, 3), DateTimeKind.Utc),
            ModifiedDate = DateTime.SpecifyKind(new DateTime(2024, 1, 4), DateTimeKind.Utc),
            PreserveExecutedInputs = false,
        };
    }
}
