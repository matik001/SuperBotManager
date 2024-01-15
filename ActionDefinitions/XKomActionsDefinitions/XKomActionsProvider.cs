using SuperBotManagerBase.Attributes;
using SuperBotManagerBase.DB.Repositories;

namespace XKomActionsDefinitions
{
    [ActionsDefinitionProvider("Discord")]
    public class XKomActionsProvider
    {
        public static ActionDefinition OpenBoxes { get; } = new ActionDefinition()
        {
            ActionDefinitionQueueName = "xkom-open-boxes",
            ActionDefinitionName = "XKom - open boxes",
            ActionDefinitionDescription = "Open boxes from x-kom",
            ActionDefinitionIcon = "https://prowly-uploads.s3.eu-west-1.amazonaws.com/uploads/6576/assets/175470/original-8aea4d597b511183a66479766d644616.jpg",
            ActionDataSchema = new ActionDefinitionSchema()
            {
                InputSchema = new List<FieldInfo>()
                {
                    new FieldInfo("Email", FieldType.String, "Email for xkom")
                    {
                        Placeholder = "Enter an email"
                    },
                    new FieldInfo("Password", FieldType.Secret, "Password for xkom")
                    {
                        Placeholder = "Enter a password"
                    },
                },
                OutputSchema = new List<FieldInfo>()
                {
                    new FieldInfo("Box1", FieldType.String, "Won equipment from box 1"),
                    new FieldInfo("Box2", FieldType.String, "Won equipment from box 2"),
                    new FieldInfo("Box3", FieldType.String, "Won equipment from box 3"),
                },
            },
            CreatedDate = DateTime.SpecifyKind(new DateTime(2024, 1, 10), DateTimeKind.Utc),
            ModifiedDate = DateTime.SpecifyKind(new DateTime(2024, 1, 10), DateTimeKind.Utc),
            PreserveExecutedInputs = true,
        };
    }
}
