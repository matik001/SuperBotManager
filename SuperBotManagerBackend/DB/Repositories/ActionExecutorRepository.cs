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
    public class ActionExecutor : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string ActionExecutorName { get; set; }

        public ActionExecutorSchema ActionData { get; set; }

        [ForeignKey("ActionDefinition")]
        public int ActionDefinitionId { get; set; }
        public virtual ActionDefinition ActionDefinition{ get; set; }

        public RunPeriod RunPeriod { get; set; } = RunPeriod.Manual;
        public DateTime? LastRunDate { get; set; }
        public int? TimeIntervalSeconds { get; set; }

        [ForeignKey("ActionExecutorOnFinish")]
        public int? ActionExecutorOnFinishId { get; set; }
        public virtual ActionExecutor ActionExecutorOnFinish { get; set; }


        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    public interface IActionExecutorRepository : IGenericRepository<ActionExecutor>
    {

    }
    public class ActionExecutorRepository : GenericRepository<ActionExecutor>, IActionExecutorRepository
    {
        public ActionExecutorRepository(AppDBContext dbContext) : base(dbContext)
        {
        }
    }
}
