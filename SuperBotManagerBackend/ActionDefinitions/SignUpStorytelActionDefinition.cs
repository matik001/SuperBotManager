using SuperBotManagerBackend.ActionExecutors;
using SuperBotManagerBackend.DB.Repositories;

namespace SuperBotManagerBackend.BotDefinitions
{
    [ActionDefinition]
    public class SignUpStorytelActionDefinition
    {
        public static ActionDefinition ActionDefinition { get; } = new ActionDefinition()
        {
            ActionDefinitionName = "SignUpStorytel",
            ActionDefinitionDescription = "Create an account in storytel",
            ActionDefinitionIcon = "storytel.png",
            ActionDataSchema = new ActionDefinitionSchema()
            {
                InputSchema = new List<FieldInfo>()
                {
                    new FieldInfo("Email", FieldType.String, "Address email for your new account"),
                    new FieldInfo("Password", FieldType.String, "Password for your new account"),
                    new FieldInfo("CardNumber", FieldType.String, "Card number eg. 1234 1234 1234 1234"),
                    new FieldInfo("CardCCV", FieldType.Number, "Card CCV numer eg. 321"),
                    new FieldInfo("CardExpiration", FieldType.String, "Card Expiration with format MM/YY eg. 07/25"),
                },
                OutputSchema = new List<FieldInfo>()
                {
                    new FieldInfo("Successful", FieldType.Boolean, "True if account was created"),
                    new FieldInfo("Message", FieldType.String, "Result message"),
                },
            },
            CreatedDate = DateTime.SpecifyKind(new DateTime(2024, 1, 3), DateTimeKind.Utc),
            ModifiedDate = DateTime.SpecifyKind(new DateTime(2024, 1, 4), DateTimeKind.Utc)
        };
    }
}
