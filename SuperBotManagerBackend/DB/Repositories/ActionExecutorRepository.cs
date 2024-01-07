using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using SuperBotManagerBackend.Configuration;
using SuperBotManagerBackend.DTOs;
using SuperBotManagerBackend.Utils;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace SuperBotManagerBackend.DB.Repositories
{
    public class ActionExecutorSchema 
    {
        public List<Dictionary<string, string>> Inputs { get; set; } = new List<Dictionary<string, string>>();

        public ActionExecutorSchema DeepClone()
        {
            return new ActionExecutorSchema()
            {
                Inputs = this.Inputs.Select(a => a.ToDictionary(b => b.Key, b => b.Value)).ToList()
            };
        }

        ///////////////////////////////////////////////////////////////////////////////////////// UTILS
        public static readonly string DEFAULT_SECRET_MASK = "????????";
        public void MaskSecrets(ActionDefinitionSchema schema)
        {
            var secretFields = schema.InputSchema.Where(a => a.Type == FieldType.Secret);

            foreach (var input in Inputs)
            {
                foreach(var secretField in secretFields)
                {
                    if(input.ContainsKey(secretField.Name))
                    {
                        input[secretField.Name] = DEFAULT_SECRET_MASK;
                    }
                }
            }
        }


        /// <summary>
        /// it replaces masked field with value from "original" (for example secret guid)
        /// </summary>
        public async Task ReverseMasking(ActionExecutorSchema original, ActionDefinitionSchema definitionSchema)
        {
            var secretFields = definitionSchema.InputSchema.Where(a => a.Type == FieldType.Secret);

            
            for(int i = 0; i< Inputs.Count; i++)
            {
                var input = Inputs[i];
                foreach(var secretField in secretFields)
                {
                    if(input.ContainsKey(secretField.Name))
                    {
                        var plainTextValue = input[secretField.Name];
                        if(plainTextValue != DEFAULT_SECRET_MASK)
                            continue;
                        input[secretField.Name] = original.Inputs[i][secretField.Name];
                    }
                }
            }
        }



        /// / <summary>
        /// if secret input value is not equal mask it will encrypt it and replace with secret id. You must save changes!
        /// </summary>
        /// <param name="definitionSchema"></param>
        /// <param name="uow"></param>
        /// <returns></returns>chema, IAppUnitOfWork uow)
        public async Task EncryptNotMaskedSecrets(ActionDefinitionSchema definitionSchema, IAppUnitOfWork uow)
        {
            var secretFields = definitionSchema.InputSchema.Where(a => a.Type == FieldType.Secret);

            foreach(var input in Inputs)
            {
                foreach(var secretField in secretFields)
                {
                    if(input.ContainsKey(secretField.Name))
                    {
                        var plainTextValue = input[secretField.Name];
                        if(plainTextValue == DEFAULT_SECRET_MASK)
                            continue;
                        var newSecret = new Secret()
                        {
                            Id = Guid.NewGuid(),
                            DecryptedSecretValue = plainTextValue
                        };
                        await uow.SecretRepository.Create(newSecret);
                        input[secretField.Name] = newSecret.Id.ToString();
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
                        var secretGuid = input[secretField.Name];
                        if(secretGuid == null)
                            continue; 
                        var secret = await uow.SecretRepository.GetById(Guid.Parse(secretGuid));
                        if(secret != null)
                            continue;
                        input[secretField.Name] = secret.DecryptedSecretValue;
                    }
                }
            }
        }
    }
    public enum RunPeriod
    {
        Manual, Everyday, Loop, TimePeriod
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

        public RunPeriod RunPeriod { get; set; } = RunPeriod.Manual;
        public DateTime? LastRunDate { get; set; }
        public int? TimeIntervalSeconds { get; set; }

        [ForeignKey("ActionExecutorOnFinish")]
        public int? ActionExecutorOnFinishId { get; set; }
        public virtual ActionExecutor ActionExecutorOnFinish { get; set; }


        public static bool CheckIfValid(ActionExecutor actionExecutor)
        {
            var actionDefinition = actionExecutor.ActionDefinition;
            var actionData = actionExecutor.ActionData;
            if (actionDefinition == null || actionData == null)
                return false;
            if (actionData.Inputs.Count == 0)
                return false;
            foreach (var inputSchema in actionDefinition.ActionDataSchema.InputSchema)
            {
                var values = actionData.Inputs.Select(a => a.ContainsKey(inputSchema.Name) ? a[inputSchema.Name] : null);
                if(!inputSchema.IsOptional && values.Any(a => string.IsNullOrEmpty(a)))
                    return false;                    
            }
            return true;    
        }
        public void UpdateIsValid()
        {
            if(this.ActionDefinition == null)
                throw new Exception("ActionDefinition is null");
            this.IsValid = ActionExecutor.CheckIfValid(this);
        }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    public interface IActionExecutorRepository : IGenericRepository<ActionExecutor, int>
    {
        Task LoadDefinition(ActionExecutor actionExecutor);
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
    }
}
