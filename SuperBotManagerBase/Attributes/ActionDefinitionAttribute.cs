namespace SuperBotManagerBase.Attributes
{
    public class ActionsDefinitionProviderAttribute : Attribute
    {
        public string ActionsGroupName { get; set; }
        public ActionsDefinitionProviderAttribute(string actionsGroupName)
        {
            ActionsGroupName = actionsGroupName;
        }
    }

}
