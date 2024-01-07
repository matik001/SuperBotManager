using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using SuperBotManagerBackend.DTOs;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace SuperBotManagerBackend.DB.Repositories
{
    public class ActionExecutorSchema
    {
        public List<Dictionary<string, string>> Inputs { get; set; } = new List<Dictionary<string, string>>();
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
