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
        public static ActionDefinition LightChange{ get; } = new ActionDefinition()
        {
            ActionDefinitionQueueName = "light-change",
            ActionDefinitionName = "BleBox - change light",
            ActionDefinitionDescription = "Changes light's color",
            ActionDefinitionIcon = "https://blebox.eu/wp-content/uploads/1600-1600-max.png",
            ActionDataSchema = new ActionDefinitionSchema()
            {
                InputSchema = new List<FieldInfo>()
                {
                    new FieldInfo("BleBoxIP", FieldType.String, "IP of BleBox device")
                    {
                        Placeholder = "Enter an IP"
                    },
                    new FieldInfo("Color", FieldType.String, "New color")
                    {
                        Placeholder = "Enter a color i.e. 4300000000",
                        IsOptional = true
                    },
                    new FieldInfo("FadeMs", FieldType.Number, "Fade amount in ms")
                    {
                        Placeholder = "Enter a fade time in ms",
                        IsOptional = true
                    },

                    new FieldInfo("ForTime", FieldType.Number, "How long light should be set?")
                    {
                        Placeholder = "Enter a time for what light will be set",
                        IsOptional = true
                    },

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
