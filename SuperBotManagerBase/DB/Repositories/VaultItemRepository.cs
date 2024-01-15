using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBotManagerBase.DB.Repositories
{

    [Table("vaultitem")]
    public class VaultItem : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        
        public string VaultGroupName { get; set; }

        public string FieldName { get; set; }

        [ForeignKey("Secret")]
        public Guid? SecretId { get; set; }
        public virtual Secret? Secret { get; set; }


        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
        public virtual User Owner { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
    public interface IVaultItemRepository : IGenericRepository<VaultItem, int>
    {
        public Task CreateMissingVaultItems(int userId, IEnumerable<ActionDefinition> definitions);
        public void RemoveUserVaultItems(int userId);
    }
    public class VaultItemRepository : GenericRepository<VaultItem, int>, IVaultItemRepository
    {
        public VaultItemRepository(AppDBContext dbContext) : base(dbContext)
        {
        }
        public void RemoveUserVaultItems(int userId)
        {
            var userVaults = GetAll().Where(a => a.OwnerId == userId).ToList();
            _dbContext.VaultItems.RemoveRange(userVaults);
        }
        public async Task CreateMissingVaultItems(int userId, IEnumerable<ActionDefinition> definitions)
        {
            var userVaults = GetAll().Where(a => a.OwnerId == userId).ToList();
            var groupedVaults = userVaults.GroupBy(a => a.VaultGroupName)
                                          .ToDictionary(a=>a.Key, g=>g.Select(a=>a.FieldName).ToHashSet());
            foreach (var definition in definitions) {
                foreach(var field in definition.ActionDataSchema.InputSchema)
                {
                    if(field.Type != FieldType.Secret)
                        continue;
                    if(groupedVaults.ContainsKey(definition.ActionDefinitionGroup)
                        && groupedVaults[definition.ActionDefinitionGroup].Contains(field.Name))
                        continue;
                    if(!groupedVaults.ContainsKey(definition.ActionDefinitionGroup))
                        groupedVaults[definition.ActionDefinitionGroup] = new HashSet<string>();
                    groupedVaults[definition.ActionDefinitionGroup].Add(field.Name);

                    var vaultItem = new VaultItem()
                    {
                        FieldName = field.Name,
                        OwnerId = userId,
                        SecretId = null,
                        VaultGroupName = definition.ActionDefinitionGroup
                    };
                    await Create(vaultItem);
                }
            }
        }

    }
}
