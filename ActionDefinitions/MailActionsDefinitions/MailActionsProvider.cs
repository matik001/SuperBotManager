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
            ActionDefinitionName = "Mail - Read one",
            ActionDefinitionDescription = "Read one mail and mark it as read",
            ActionDefinitionIcon = "/mail.png",
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
                        Placeholder = "Enter a mailbox",
                        InitialValue = "Inbox",
                    },
                    new FieldInfo("SearchPhrase", FieldType.String, "Phrase to search")
                    {
                        Placeholder = "Enter a phrase",
                        IsOptional = true,
                    },
                },
                OutputSchema = new List<FieldInfo>()
                {
                    new FieldInfo("From", FieldType.String, "From whom you got the mail"),
                    new FieldInfo("FromName", FieldType.String, "From whom you got the mail"),
                    new FieldInfo("Subject", FieldType.String, "Subject of the mail"),
                    new FieldInfo("TextBody", FieldType.String, "Body of the mail (text format)"),
                    new FieldInfo("HtmlBody", FieldType.String, "Body of the mail (html format)"),
                },
            },
            CreatedDate = DateTime.SpecifyKind(new DateTime(2024, 2, 4), DateTimeKind.Utc),
            ModifiedDate = DateTime.SpecifyKind(new DateTime(2024, 2, 4), DateTimeKind.Utc),
            PreserveExecutedInputs = true,
        };


        public static ActionDefinition MailSend { get; } = new ActionDefinition()
        {
            ActionDefinitionQueueName = "mail-send",
            ActionDefinitionName = "Mail - Send",
            ActionDefinitionDescription = "Send a mail",
            ActionDefinitionIcon = "/mail.png",
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
                    new FieldInfo("To", FieldType.String, "To whom You want to send an email?")
                    {
                        Placeholder = "Enter an email"
                    },
                    new FieldInfo("Subject", FieldType.String, "Subject of the mail")
                    {
                        Placeholder = "Enter a subject"
                    },
                    new FieldInfo("Body", FieldType.String, "Body of the mail")
                    {
                        Placeholder = "Enter body of the message"
                    },
                },
                OutputSchema = new List<FieldInfo>()
                {
                    new FieldInfo("Server reponse", FieldType.String, "Server's free reponse"),
                },
            },
            CreatedDate = DateTime.SpecifyKind(new DateTime(2024, 2, 4), DateTimeKind.Utc),
            ModifiedDate = DateTime.SpecifyKind(new DateTime(2024, 2, 4), DateTimeKind.Utc),
            PreserveExecutedInputs = true,
        };
    }
}
