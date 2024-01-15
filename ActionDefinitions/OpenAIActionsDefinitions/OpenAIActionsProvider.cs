using SuperBotManagerBase.Attributes;
using SuperBotManagerBase.DB.Repositories;

namespace OpenAIActionsDefinitions
{
    [ActionsDefinitionProvider("OpenAI")]
    public class OpenAIActionsProvider
    {
        public static ActionDefinition AskGpt { get; } = new ActionDefinition()
        {
            ActionDefinitionQueueName = "openai-gpt-api",
            ActionDefinitionName = "OpenAI - GPT API",
            ActionDefinitionDescription = "Get an answer from GPT",
            ActionDefinitionIcon = "https://static.vecteezy.com/system/resources/previews/021/059/825/original/chatgpt-logo-chat-gpt-icon-on-green-background-free-vector.jpg",
            ActionDataSchema = new ActionDefinitionSchema()
            {
                InputSchema = new List<FieldInfo>()
                {
                    new FieldInfo("Key", FieldType.Secret, "OpenAI API key")
                    {
                        Placeholder = "Enter an API key"
                    },
                    new FieldInfo("System message", FieldType.String, "Tell ai what type of task it should perform", true)
                    {
                        Placeholder = "Enter a system message"
                    },
                    new FieldInfo("Question", FieldType.String, "Question you want to ask an AI model")
                    {
                        Placeholder = "Enter a question"
                    },
                    new FieldInfo("Temperature", FieldType.Number, "How much creative model should be (between 0 and 1)")
                    {
                        Placeholder = "Enter a temperature",
                        InitialValue = "0.3",
                    },
                    new FieldInfo("Model", FieldType.Set, "Picker a model you want to use.", true)
                    {
                        SetOptions = new List<SetOption>()
                        {
                            new SetOption("GPT3.5", "GPT3.5"),
                            new SetOption("GPT4", "GPT4"),
                        },
                        InitialValue = "GPT3.5",
                    },
                },
                OutputSchema = new List<FieldInfo>()
                {
                    new FieldInfo("Response", FieldType.String, "Answer from an AI model"),
                },
            },
            CreatedDate = DateTime.SpecifyKind(new DateTime(2024, 1, 10), DateTimeKind.Utc),
            ModifiedDate = DateTime.SpecifyKind(new DateTime(2024, 1, 10), DateTimeKind.Utc),
            PreserveExecutedInputs = true,
        };
    }
}
