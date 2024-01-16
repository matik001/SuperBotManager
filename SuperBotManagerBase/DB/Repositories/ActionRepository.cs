using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace SuperBotManagerBase.DB.Repositories
{
    public class ActionSchema
    {
        public Dictionary<string, string> Input { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> Output { get; set; } = new Dictionary<string, string>();

        public static async Task<Dictionary<string, string>> EncryptDict(IAppUnitOfWork uow, Dictionary<string, string> data, List<FieldInfo> def)
        {
            var resDict = new Dictionary<string, string>();
            foreach(var item in data)
            {
                if(def.Any(a => a.Name == item.Key && a.Type == FieldType.Secret))
                {
                    var secret = new Secret()
                    {
                        Id = Guid.NewGuid(),
                        DecryptedSecretValue = item.Value
                    };
                    await uow.SecretRepository.Create(secret);
                    resDict.Add(item.Key, secret.Id.ToString());
                }
                else
                {
                    resDict.Add(item.Key, item.Value);
                }

            }
            return resDict;
        }

        /// <summary>
        ///  Save uow
        /// </summary>
        public static async Task<ActionSchema> Encrypt(IAppUnitOfWork uow, ActionSchema data, ActionDefinitionSchema def)
        {
            var res = new ActionSchema();
            res.Input = await EncryptDict(uow, data.Input, def.InputSchema);
            res.Output = await EncryptDict(uow, data.Output, def.OutputSchema);
            return res;
        }
        public static async Task<Dictionary<string, string>> DecryptDict(IAppUnitOfWork uow, Dictionary<string, string> dict, List<FieldInfo> def)
        {
            var resDict = new Dictionary<string, string>();

            foreach(var item in dict)
            {
                if(def.Any(a => a.Name == item.Key && a.Type == FieldType.Secret))
                {
                    var secret = await uow.SecretRepository.GetById(Guid.Parse(item.Value));
                    resDict.Add(item.Key, secret.DecryptedSecretValue);
                }
                else
                {
                    resDict.Add(item.Key, item.Value);
                }
            }
            return resDict;
        }
        public static async Task<ActionSchema> Decrypt(IAppUnitOfWork uow, ActionSchema data, ActionDefinitionSchema def)
        {
            var res = new ActionSchema();
            res.Input = await DecryptDict(uow, data.Input, def.InputSchema);
            res.Output = await DecryptDict(uow, data.Output, def.OutputSchema);
            return res;
        }


    }

    public enum ActionStatus
    {
        Pending, InProgress, Finished, Error
    }
    public enum RunStartType
    {
        Manual, Scheduled
    }
    [Table("action")]
    public class Action : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        public ActionSchema ActionData { get; set; }
        public ActionStatus ActionStatus { get; set; } = ActionStatus.Pending;
        public RunStartType RunStartType { get; set; } = RunStartType.Manual;

        [ForeignKey("ActionExecutor")]
        public int? ActionExecutorId { get; set; }
        public virtual ActionExecutor? ActionExecutor { get; set; }


        [ForeignKey("ActionOnFinish")]
        public int? ActionOnFinishId { get; set; }
        public virtual Action ActionOnFinish { get; set; }



        public int? ErrorId { get; set; }
        /// TODO Error object

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    public interface IActionRepository : IGenericRepository<Action, int>
    {

    }
    public class ActionRepository : GenericRepository<Action, int>, IActionRepository
    {
        public ActionRepository(AppDBContext dbContext) : base(dbContext)
        {
        }
    }
}
