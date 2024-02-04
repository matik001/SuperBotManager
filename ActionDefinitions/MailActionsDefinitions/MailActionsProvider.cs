using SuperBotManagerBase.Attributes;
using SuperBotManagerBase.DB.Repositories;

namespace XKomActionsDefinitions
{
    [ActionsDefinitionProvider("Mail")]
    public class MailActionsProvider
    {
        public static ActionDefinition MailRead { get; } = new ActionDefinition()
        {
            ActionDefinitionQueueName = "mail-read",
            ActionDefinitionName = "Mail - read one",
            ActionDefinitionDescription = "Read one mail and mark it as read",
            ActionDefinitionIcon = "https://play-lh.googleusercontent.com/D1Dz2BjPYev_oyksKXsdtAS66a_2Ql-sklpzTnwR9lqnDG_P5lAJEtfR70FudJ0XMA",
            ActionDataSchema = new ActionDefinitionSchema()
            {
                InputSchema = new List<FieldInfo>()
                {
                    new FieldInfo("Host", FieldType.String, "Mail server url")
                    {
                        Placeholder = "Enter mail server url"
                    },
                    new FieldInfo("Port", FieldType.String, "Server port")
                    {
                        Placeholder = "Enter a port number"
                    },
                    new FieldInfo("SSL", FieldType.Boolean, "Should use SSL?")
                    {
                    },
                    new FieldInfo("Login", FieldType.String, "Login for server")
                    {
                        Placeholder = "Enter login"
                    },
                    new FieldInfo("Password", FieldType.Secret, "Password for user")
                    {
                        Placeholder = "Enter password"
                    },
                    new FieldInfo("MailBox", FieldType.String, "Mailbox for mails")
                    {
                        Placeholder = "Enter a mailbox"
                    },
                    new FieldInfo("SearchPhrase", FieldType.String, "Phrase to search")
                    {
                        InitialValue = "Inbox",
                        Placeholder = "Enter a phrase"
                    },
                },
                OutputSchema = new List<FieldInfo>()
                {
                    new FieldInfo("From", FieldType.String, "From whom you got the mail"),
                    new FieldInfo("Subject", FieldType.String, "Subject of the mail"),
                    new FieldInfo("Body", FieldType.String, "Body of the mail"),
                },
            },
            CreatedDate = DateTime.SpecifyKind(new DateTime(2024, 2, 4), DateTimeKind.Utc),
            ModifiedDate = DateTime.SpecifyKind(new DateTime(2024, 2, 4), DateTimeKind.Utc),
            PreserveExecutedInputs = true,
        };
    }
}
