using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperBotManagerBackend.ActionTemplates;
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
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsDefined(typeof(ActionDefinitionAttribute)));
            var actionDefinitions = types.Select(a =>
            {
                var action = a.GetProperty("ActionDefinition", BindingFlags.Static | BindingFlags.Public).GetValue(null, null) as ActionDefinition;
                action.Id = action.ActionDefinitionName.GetHashCode();
                return action;
            });
            builder.HasData(actionDefinitions);
        }
    }
}
