﻿using SuperBotManagerBase.Attributes;
using SuperBotManagerBase.DB.Repositories;

namespace DiscordActionsDefinitions
{
    [ActionsDefinitionProvider]
    public class DiscordActionsProvider
    {
        public static ActionDefinition SendMessage { get; } = new ActionDefinition()
        {
            ActionDefinitionQueueName = "discord-send-message",
            ActionDefinitionName = "Discord - send a message",
            ActionDefinitionDescription = "Send message in discord",
            ActionDefinitionIcon = "https://images-eds-ssl.xboxlive.com/image?url=4rt9.lXDC4H_93laV1_eHHFT949fUipzkiFOBH3fAiZZUCdYojwUyX2aTonS1aIwMrx6NUIsHfUHSLzjGJFxxsG72wAo9EWJR4yQWyJJaDb6rYcBtJvTvH3UoAS4JFNDaxGhmKNaMwgElLURlRFeVkLCjkfnXmWtINWZIrPGYq0-&format=source",
            ActionDataSchema = new ActionDefinitionSchema()
            {
                InputSchema = new List<FieldInfo>()
                {
                    new FieldInfo("Message", FieldType.String, "What message do you want to send?"),
                    new FieldInfo("Tag everyone", FieldType.Boolean, "Do you want to tag @everyone?"),
                    new FieldInfo("Token", FieldType.Secret, "How to get it: https://discordnet.dev/guides/getting_started/first-bot.html"),
                },
                OutputSchema = new List<FieldInfo>()
                {
                },
            },
            CreatedDate = DateTime.SpecifyKind(new DateTime(2024, 1, 8), DateTimeKind.Utc),
            ModifiedDate = DateTime.SpecifyKind(new DateTime(2024, 1, 8), DateTimeKind.Utc),
            PreserveExecutedInputs = true
        };
    }
}
