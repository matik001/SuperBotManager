using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace SuperBotManagerBase.DB.Repositories
{
    public class FieldValue
    {
        public string Value { get; set; }
        public bool IsEncrypted { get; set; }
        public bool IsValid { get; set; }

        public FieldValue(string value, bool isMasked = false, bool isValid = true)
        {
            Value = value;
            IsEncrypted = isMasked;
            IsValid = isValid;
        }

        public FieldValue Clone()
        {
            return new FieldValue(Value, IsEncrypted, IsValid);
        }
    }
    public class ActionExecutorSchema 
    {
        /// <summary>
        /// paris of field name - value
        /// </summary>
        public List<Dictionary<string, FieldValue>> Inputs { get; set; } = new List<Dictionary<string, FieldValue>>();

        public ActionExecutorSchema DeepClone()
        {
            return new ActionExecutorSchema()
            {
                Inputs = this.Inputs.Select(a => a.ToDictionary(b => b.Key, b => b.Value.Clone())).ToList()
            };
        }



        public async Task EncryptSecrets(ActionDefinitionSchema definitionSchema, IAppUnitOfWork uow)
        {
            var secretFields = definitionSchema.InputSchema.Where(a => a.Type == FieldType.Secret);

            foreach(var input in Inputs)
            {
                foreach(var secretField in secretFields)
                {
                    if(input.ContainsKey(secretField.Name) && input[secretField.Name] != null && !input[secretField.Name].IsEncrypted)
                    {
                        var plainTextValue = input[secretField.Name].Value;
                        var newSecret = new Secret()
                        {
                            Id = Guid.NewGuid(),
                            DecryptedSecretValue = plainTextValue
                        };
                        await uow.SecretRepository.Create(newSecret);
                        input[secretField.Name].Value = newSecret.Id.ToString();
                        input[secretField.Name].IsEncrypted = true;
                    }
                }
            }
        }

        public async Task DecryptSecrets(ActionDefinitionSchema definitionSchema, IAppUnitOfWork uow)
        {
            var secretFields = definitionSchema.InputSchema.Where(a => a.Type == FieldType.Secret);

            foreach(var input in Inputs)
            {
                foreach(var secretField in secretFields)
                {
                    if(input.ContainsKey(secretField.Name))
                    {
                        var secretGuid = input[secretField.Name]?.Value;
                        if(secretGuid == null)
                            continue;
                        if(input[secretField.Name].IsEncrypted == false)
                            continue;
                        var secret = await uow.SecretRepository.GetById(Guid.Parse(secretGuid));
                        if(secret == null)
                            continue;
                        input[secretField.Name].Value = secret.DecryptedSecretValue;
                        input[secretField.Name].IsEncrypted = false;
                    }
                }
            }
        }
    }
    public enum RunMethod
    {
        Manual, Automatic, TimePeriod
    }

    [Table("actionexecutor")]
    public class ActionExecutor : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        public string ActionExecutorName { get; set; }

        public ActionExecutorSchema ActionData { get; set; }

        public bool PreserveExecutedInputs { get; set; }
        public bool IsValid { get; set; } /// all inputs are filled correctly

        [ForeignKey("ActionDefinition")]
        public int ActionDefinitionId { get; set; }
        public virtual ActionDefinition ActionDefinition{ get; set; }

        public RunMethod RunMethod { get; set; } = RunMethod.Manual;
        public DateTime? LastRunDate { get; set; }
        public int? TimeIntervalSeconds { get; set; }

        [ForeignKey("ActionExecutorOnFinish")]
        public int? ActionExecutorOnFinishId { get; set; }
        public virtual ActionExecutor ActionExecutorOnFinish { get; set; }

        public virtual ICollection<Action> Actions { get; set; }

        public void UpdateIsValid()
        {
            if(this.ActionDefinition == null)
                throw new Exception("ActionDefinition is null");

            var actionDefinition = this.ActionDefinition;
            var actionData = this.ActionData;
            if (actionDefinition == null || actionData == null)
                return;
            if (actionData.Inputs.Count == 0)
                return;

            this.IsValid = true;
            foreach (var fieldInfo in actionDefinition.ActionDataSchema.InputSchema)
            {
                foreach (var input in actionData.Inputs)
                {
                    if(!fieldInfo.IsOptional)
                    {
                        if(!input.ContainsKey(fieldInfo.Name) || input[fieldInfo.Name] == null)
                        {
                            this.IsValid = false;
                            continue;
                        }
                        var fieldValue = input[fieldInfo.Name];
                        if(string.IsNullOrEmpty(fieldValue.Value))
                        {
                            fieldValue.IsValid = false;
                            this.IsValid = false;
                            continue;
                        }

                    }
                }
            }
        }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    public interface IActionExecutorRepository : IGenericRepository<ActionExecutor, int>
    {
        Task LoadDefinition(ActionExecutor actionExecutor);
        Task LoadExecutorOnFinish(ActionExecutor actionExecutor);
    }
    public class ActionExecutorRepository : GenericRepository<ActionExecutor, int>, IActionExecutorRepository
    {
        public ActionExecutorRepository(AppDBContext dbContext) : base(dbContext)
        {
        }

        public async Task LoadDefinition(ActionExecutor actionExecutor)
        {
            await this._dbContext.Entry(actionExecutor).Reference(a => a.ActionDefinition).LoadAsync();
        }
        public async Task LoadExecutorOnFinish(ActionExecutor actionExecutor)
        {
            await this._dbContext.Entry(actionExecutor).Reference(a => a.ActionExecutorOnFinish).LoadAsync();
        }
    }
}
