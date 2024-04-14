using SuperBotManagerBase.Attributes;
using SuperBotManagerBase.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BleBoxActionsDefinitions
{
    [ActionsDefinitionProvider("BleBox")]
    public class BleBoxActionsProvider
    {
        public static ActionDefinition LightSwitch { get; } = new ActionDefinition()
        {
            ActionDefinitionQueueName = "light-switch",
            ActionDefinitionName = "BleBox - switch light",
            ActionDefinitionDescription = "Switches light",
            ActionDefinitionIcon = "https://blebox.eu/wp-content/uploads/1600-1600-max.png",
            ActionDataSchema = new ActionDefinitionSchema()
            {
                InputSchema = new List<FieldInfo>()
                {
                    new FieldInfo("BleBoxIP", FieldType.String, "IP of BleBox device")
                    {
                        Placeholder = "Enter an IP"
                    }
                },
                OutputSchema = new List<FieldInfo>()
                {
                },
            },
            CreatedDate = DateTime.SpecifyKind(new DateTime(2024, 4, 14), DateTimeKind.Utc),
            ModifiedDate = DateTime.SpecifyKind(new DateTime(2024, 4, 14), DateTimeKind.Utc),
            PreserveExecutedInputs = true
        };

    }
}
