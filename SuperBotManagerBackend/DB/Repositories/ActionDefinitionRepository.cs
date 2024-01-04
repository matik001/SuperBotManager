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
    public class ActionDefinitionSchema
    {
        public Dictionary<string, string> Input { get; set; } = new Dictionary<string, string>();
    }
    public enum RunPeriod
    {
        Single, Everydaym, NonStop, TimePeriod
    }

    [Table("actiondefinition")]
    public class ActionDefinition : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string ActionDefinitionName { get; set; }

        public ActionDefinitionSchema ActionData { get; set; }

        [ForeignKey("ActionTemplate")]
        public int ActionTemplateId { get; set; }
        public virtual ActionTemplate ActionTemplate{ get; set; }

        public RunPeriod RunPeriod { get; set; } = RunPeriod.Single;
        public DateTime? LastRunDate { get; set; }
        public int? TimeIntervalSeconds { get; set; }

        [ForeignKey("ActionDefinitionOnFinish")]
        public int? ActionDefinitionOnFinishId { get; set; }
        public ActionDefinition? ActionDefinitionOnFinish { get; set; }


        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    public interface IActionDefinitionRepository : IGenericRepository<ActionDefinition>
    {

    }
    public class ActionDefinitionRepository : GenericRepository<ActionDefinition>, IActionDefinitionRepository
    {
        public ActionDefinitionRepository(AppDBContext dbContext) : base(dbContext)
        {
        }
    }
}
