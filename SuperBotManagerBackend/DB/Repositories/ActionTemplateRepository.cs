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
    public class ActionTemplateSchema
    {
        public Dictionary<string, string> Input { get; set; } = new Dictionary<string, string>();
    }
    public enum RunPeriod
    {
        Single, Everydaym, NonStop, TimePeriod
    }

    [Table("actiontemplate")]
    public class ActionTemplate : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string ActionTemplateName { get; set; }

        public ActionTemplateSchema ActionData { get; set; }

        [ForeignKey("ActionDefinition")]
        public int ActionDefinitionId { get; set; }
        public virtual ActionDefinition ActionDefinition{ get; set; }

        public RunPeriod RunPeriod { get; set; } = RunPeriod.Single;
        public DateTime? LastRunDate { get; set; }
        public int? TimeIntervalSeconds { get; set; }

        [ForeignKey("ActionTemplateOnFinish")]
        public int? ActionTemplateOnFinishId { get; set; }
        public virtual ActionTemplate? ActionTemplateOnFinish { get; set; }


        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    public interface IActionTemplateRepository : IGenericRepository<ActionTemplate>
    {

    }
    public class ActionTemplateRepository : GenericRepository<ActionTemplate>, IActionTemplateRepository
    {
        public ActionTemplateRepository(AppDBContext dbContext) : base(dbContext)
        {
        }
    }
}
