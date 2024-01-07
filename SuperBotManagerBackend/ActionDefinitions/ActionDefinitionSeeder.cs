using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperBotManagerBackend.ActionExecutors;
using SuperBotManagerBackend.BotDefinitions;
using SuperBotManagerBackend.DB.Repositories;
using System.Diagnostics;
using System.Reflection;

namespace SuperBotManagerBackend.ActionDefinitions
{
    public class ActionDefinitionSeeder
    {
        public static void Seed(EntityTypeBuilder<ActionDefinition> builder)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsDefined(typeof(ActionsDefinitionProviderAttribute)));
            var actionDefinitions = types.SelectMany(provider =>
            {
                var properties = provider.GetProperties().Where(p => p.PropertyType == typeof(ActionDefinition));
                return properties.Select(p =>
                {
                    var action = p.GetValue(null, null) as ActionDefinition;
                    action.Id = action.ActionDefinitionName.GetHashCode();
                    return action;
                });
            });
            builder.HasData(actionDefinitions);
        }
    }
}
