using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using SuperBotManagerBase.Attributes;
using SuperBotManagerBase.DB;
using SuperBotManagerBase.DB.Repositories;
using SuperBotManagerBase.Utils;
using System.Diagnostics;
using System.Reflection;

namespace SuperBotManagerBase.Services
{
    public interface IActionDefinitionSeederService
    {
        Task Seed(bool removeUnpresent = false);
    }
    public class ActionDefinitionSeederService : IActionDefinitionSeederService
    {
        private readonly IAppUnitOfWork uow;
        private readonly ILogger<ActionDefinitionSeederService> logger;

        public ActionDefinitionSeederService(IAppUnitOfWork uow, ILogger<ActionDefinitionSeederService> logger)
        {
            this.uow = uow;
            this.logger = logger;
        }

        public int GetDeterministicHashCode(string str)
        {
            int hash = 0;
            foreach (char c in str)
            {
                hash = (hash * 13 + c) % 100000007;
            }
            return hash;
        }
        public async Task Seed(bool removeUnpresent = false)
        {
            const string assemblySuffix = "ActionsDefinitions";
            var loadedAssemblies = AssemblyUtils.LoadAssembliesContainingName(assemblySuffix);

            var providersTypes = loadedAssemblies
                .SelectMany(a => a.GetTypes().Where(t => t.IsDefined(typeof(ActionsDefinitionProviderAttribute)))).ToList();

            var actionDefinitions = providersTypes.SelectMany(provider =>
            {
                var group = provider.GetCustomAttribute<ActionsDefinitionProviderAttribute>().ActionsGroupName;
                var properties = provider.GetProperties().Where(p => p.PropertyType == typeof(ActionDefinition));
                return properties.Select(p =>
                {
                    var action = p.GetValue(null, null) as ActionDefinition;
                    action.ActionDefinitionGroup = group;
                    action.Id = GetDeterministicHashCode(action.ActionDefinitionQueueName);
                    return action;
                });
            });
            var newCount = 0;
            foreach (var actionDefinition in actionDefinitions)
            {
                var defInDB = await uow.ActionDefinitionRepository.GetById(actionDefinition.Id);
                if(defInDB == null)
                {
                    newCount++;
                    await uow.ActionDefinitionRepository.Create(actionDefinition);
                }
                else
                {
                    await uow.ActionDefinitionRepository.Update(actionDefinition);
                }
            }
            if(removeUnpresent)
            {
                uow.ActionDefinitionRepository.DeleteOthers(actionDefinitions.Select(a => a.Id));
            }

            logger.LogInformation($"Seeded action definitions ({newCount} new definitions)");
            await uow.SaveChangesAsync();
        }
    }
}
